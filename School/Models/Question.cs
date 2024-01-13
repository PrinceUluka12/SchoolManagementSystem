namespace School.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        // Navigation property - a question belongs to an exam
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        // Navigation property - a question has multiple options
        public List<Option> Options { get; set; } = new List<Option>();

        // ID of the correct option
        public int CorrectOptionId { get; set; }
    }
}
