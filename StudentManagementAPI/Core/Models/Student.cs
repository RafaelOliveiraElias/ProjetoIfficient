namespace Core.Models
{
    public class Student
    {
        public string Name { get; set; }
        public string Registration { get; set; }
        public Dictionary<string, double> Grades { get; set; }

        public double Average => Grades.Values.Average();
        public bool IsApproved => Grades.Values.All(grade => grade >= 60);
    }
}
