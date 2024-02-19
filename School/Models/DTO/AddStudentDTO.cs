namespace School.Models.DTO
{
    public class AddStudentDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CourseId {  get; set; }
    }
}
