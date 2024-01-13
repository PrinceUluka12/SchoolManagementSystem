using School.Models;

namespace School.Services.ExamSservices
{
    public interface IExamService
    {
        Task<bool> AddExam(Exam exam);
        Task<bool> UpdateExam(Exam exam);
        Task<bool> DeleteExam(int examId);
        Task<Exam> GetExamById(int examId);
        Task<List<Exam>> GetAllExams();
        Task<List<Exam>> GetExamsForCourse(int courseId);
    }
}
