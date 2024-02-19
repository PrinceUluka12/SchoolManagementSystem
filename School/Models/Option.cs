namespace School.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public string OptionText { get; set; }

        // Foreign key - an option belongs to a question
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
