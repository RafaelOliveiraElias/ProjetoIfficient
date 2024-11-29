using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface ISortStrategy
    {
        IEnumerable<Student> Sort(IEnumerable<Student> students);
    }
}