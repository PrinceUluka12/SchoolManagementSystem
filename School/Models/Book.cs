namespace School.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<Student> Borrowers { get; set; } = new List<Student>();
    }
}
