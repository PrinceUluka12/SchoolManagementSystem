namespace School.Models
{
    public class College
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Lecturer> Lecturers { get; set; } = new List<Lecturer>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Exam> Exams { get; set; } = new List<Exam>();
    }
}
