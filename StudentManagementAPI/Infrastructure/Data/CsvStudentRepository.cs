using System.Globalization;
using CsvHelper;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data.Mappings;

namespace Infrastructure.Data
{
    public class CsvStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students;

        public CsvStudentRepository(string csvFilePath)
        {
            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            // Configure o mapeamento personalizado
            csv.Context.RegisterClassMap<StudentMap>();

            // Carregar os registros
            var rawStudents = csv.GetRecords<RawStudent>().ToList();

            // Processar as notas dinamicamente
            _students = rawStudents.Select(raw => new Student
            {
                Registration = raw.Registration,
                Name = raw.Name,
                Grades = ProcessGrades(raw)
            }).ToList();
        }

        public IEnumerable<Student> GetAllStudents() => _students;

        public Student GetStudentByRegistration(string registration) =>
            _students.FirstOrDefault(s => s.Registration == registration);

        public IEnumerable<Student> GetApprovedStudents() =>
            _students.Where(s => s.IsApproved);

        public IEnumerable<Student> GetDisapprovedStudents() =>
            _students.Where(s => !s.IsApproved);

        public Dictionary<string, (Student BestStudent, double Grade)> GetBestStudentBySubject()
        {
            var bestBySubject = _students
                .SelectMany(
                    student => student.Grades,
                    (student, grade) => new { Student = student, Subject = grade.Key, Grade = grade.Value }
                )
                .GroupBy(
                    entry => entry.Subject,
                    entry => new { entry.Student, entry.Grade }
                )
                .Select(group => new
                {
                    Subject = group.Key,
                    BestStudent = group.OrderByDescending(g => g.Grade).First().Student,
                    Grade = group.Max(g => g.Grade)
                })
                .ToList(); 

            var result = bestBySubject.ToDictionary(
                x => x.Subject,
                x => (x.BestStudent, x.Grade)
            );

            return result;
        }


        public IEnumerable<Student> SortStudents(ISortStrategy strategy) =>
            strategy.Sort(_students);

        private Dictionary<string, double> ProcessGrades(RawStudent raw)
        {
            var grades = new Dictionary<string, double>();

            foreach (var property in typeof(RawStudent).GetProperties())
            {
                var name = property.Name;
                if (name != "Registration" && name != "Name")
                {
                    var value = property.GetValue(raw)?.ToString();
                    if (double.TryParse(value, out double grade))
                    {
                        grades.Add(name, grade);
                    }
                }
            }

            return grades;
        }


    }
}
