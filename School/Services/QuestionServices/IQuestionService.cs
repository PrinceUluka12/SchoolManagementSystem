using School.Models;

namespace School.Services.QuestionServices
{
    public interface IQuestionService
    {
        Task<List<Question>> GetQuestionsByExamId(int examId);
        Task<Question> GetQuestionById(int questionId);
        Task<bool> AddQuestion(Question question);
        Task<bool> UpdateQuestion(Question question);
        Task<bool> DeleteQuestion(int questionId);
    }
}
