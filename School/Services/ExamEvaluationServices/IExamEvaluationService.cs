using School.Models;

namespace School.Services.ExamEvaluationServices
{
    public interface IExamEvaluationService
    {
        int EvaluateExam(int examId, List<StudentQuestionAnswer> userAnswers);
    }
}
