using Core.Interfaces;
using Core.Models;

namespace Infrastructure.Data
{
    public class LinqSortStrategy : ISortStrategy
    {
        public IEnumerable<Student> Sort(IEnumerable<Student> students)
        {
            return students.OrderBy(s => s.Grades.Values.Average()).ToList();
        }
    }
}
