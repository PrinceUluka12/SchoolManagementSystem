using System.ComponentModel.DataAnnotations.Schema;
using static School.Static_Data.SD;

namespace School.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public CourseType CourseType { get; set; }
        public int? LecturerId { get; set; }
        [ForeignKey("LecturerId")]
        public Lecturer Lecturer { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
        public List<Exam> Exams { get; set; } = new List<Exam>();
    }
}
