using Core.Interfaces;
using Core.Models;

namespace Infrastructure.Data
{
    public class BubbleSortStrategy : ISortStrategy
    {
        public IEnumerable<Student> Sort(IEnumerable<Student> students)
        {
            var studentList = students.ToList();
            for (int i = 0; i < studentList.Count - 1; i++)
            {
                for (int j = 0; j < studentList.Count - i - 1; j++)
                {
                    if (studentList[j].Average > studentList[j + 1].Average)
                    {
                        var temp = studentList[j];
                        studentList[j] = studentList[j + 1];
                        studentList[j + 1] = temp;
                    }
                }
            }
            return studentList;
        }
    }
}
