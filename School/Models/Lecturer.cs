namespace School.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Class> Classes { get; set; } = new List<Class>();
    }
}
