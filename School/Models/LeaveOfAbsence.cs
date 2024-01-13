namespace School.Models
{
    public class LeaveOfAbsence
    {
        public int LeaveOfAbsenceId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
