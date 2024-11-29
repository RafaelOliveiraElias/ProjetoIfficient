using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using StudentManagementAPI.Services.enums;

namespace Application.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Student> GetAllStudents() => _repository.GetAllStudents();
        public IEnumerable<Student> GetApprovedStudents() => _repository.GetApprovedStudents();
        public IEnumerable<Student> GetDisapprovedStudents() => _repository.GetDisapprovedStudents();
        public Student GetStudentByRegistration(string registration) =>
            _repository.GetStudentByRegistration(registration);
        public Dictionary<string, (Student BestStudent, double Grade)> GetBestStudentBySubject() =>
            _repository.GetBestStudentBySubject();
        public IEnumerable<Student> SortStudents(ISortStrategy strategy) =>
            _repository.SortStudents(strategy);

        public IEnumerable<Student> GetSortedStudentsByAverage(SortStrategyType strategy)
        {
            var students = _repository.GetAllStudents();
            ISortStrategy sortStrategy;

            switch (strategy)
            {
                case SortStrategyType.BubbleSort:
                    sortStrategy = new BubbleSortStrategy();
                    break;
                case SortStrategyType.LinqSort:
                    sortStrategy = new LinqSortStrategy();
                    break;
                default:
                    sortStrategy = new LinqSortStrategy();
                    break;
            }

            var sorter = new StudentSorter(sortStrategy);
            return sorter.SortStudents(students);
        }
    }
}
