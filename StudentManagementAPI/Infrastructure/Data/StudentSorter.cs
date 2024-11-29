using Core.Interfaces;
using Core.Models;

public class StudentSorter
{
    private ISortStrategy _sortStrategy;

    public StudentSorter(ISortStrategy sortStrategy)
    {
        _sortStrategy = sortStrategy;
    }

    public void SetSortStrategy(ISortStrategy sortStrategy)
    {
        _sortStrategy = sortStrategy;
    }

    // Recebe IEnumerable e retorna IEnumerable após aplicar a estratégia de ordenação
    public IEnumerable<Student> SortStudents(IEnumerable<Student> students)
    {
        return _sortStrategy.Sort(students);
    }
}
