using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string MatricNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Class> Classes { get; set; } = new List<Class>();
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        [Required]
        public virtual Course Courses { get; set; }
        public List<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
        public List<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
}
