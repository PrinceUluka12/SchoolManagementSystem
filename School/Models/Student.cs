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
        public List<Course> Courses { get; set; } = new List<Course>();
        public int ClassId { get; set; } = 0;
        public virtual Class Class { get; set; }
        public List<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
        public List<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
}
