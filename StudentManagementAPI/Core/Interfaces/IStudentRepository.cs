using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentByRegistration(string registration);
        IEnumerable<Student> GetApprovedStudents();
        IEnumerable<Student> GetDisapprovedStudents();
        Dictionary<string, (Student BestStudent, double Grade)> GetBestStudentBySubject();
        IEnumerable<Student> SortStudents(ISortStrategy strategy);
    }
}