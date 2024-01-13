using System.ComponentModel.DataAnnotations;

namespace School.Models
{
    public class AttendanceRecord
    {
        [Key]
        public int AttendanceRecordId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public DateTime Date { get; set; }

        public bool IsPresent { get; set; }
    }
}
