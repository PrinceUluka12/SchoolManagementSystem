namespace School.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public List<Option> Options { get; set; } = new List<Option>();
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }

    public enum QuestionType
    {
        SingleChoice,
        MultipleChoice,
    }
}
