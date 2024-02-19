namespace School.Models
{
    public class StudentQuestionAnswer
    {
        public int QuestionId { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}



