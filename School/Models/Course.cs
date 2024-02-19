using System.ComponentModel.DataAnnotations.Schema;
using static School.Static_Data.SD;

namespace School.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<Class> Classes { get; set; } = new List<Class>();
        public List<Student> Students { get; set; } = new List<Student>();

    }
}
