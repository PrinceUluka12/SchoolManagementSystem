using System.ComponentModel.DataAnnotations.Schema;
using static School.Static_Data.SD;

namespace School.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int Credits { get; set; }
        public CourseType CourseType { get; set; }
        public int? LecturerId { get; set; }
        [ForeignKey("LecturerId")]
        public Lecturer Lecturer { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; } = new Course();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Exam> Exams { get; set; } = new List<Exam>();
    }
}
