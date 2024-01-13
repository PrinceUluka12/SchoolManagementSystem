using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public List<ExamResult> Results { get; set; } = new List<ExamResult>();
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
