using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;

namespace School.Services.ExamEvaluationServices
{
    public class ExamEvaluationService : IExamEvaluationService
    {
        private readonly AppDbContext _db;
        public ExamEvaluationService(AppDbContext db)
        {
            _db = db;
        }
        public int EvaluateExam(int examId, List<StudentQuestionAnswer> userAnswers)
        {
            var exam = _db.Exams
            .Include(e => e.Questions)
            .ThenInclude(o => o.Answers)
            .FirstOrDefault(e => e.ExamId == examId);

            if (exam == null)
            {
                // Handle exam not found
                return -1;
            }
            int totalScore = 0;


            foreach (var userAnswer in userAnswers)
            {
                var question  =  exam.Questions.FirstOrDefault(q => q.QuestionId ==  userAnswer.QuestionId);
                if(question != null)
                {
                    var correctAnswers  =  question.Answers.Select(a => a.AnswerText).ToList();
                    var userProvidedAnswers = userAnswer.Answers.Select(a => a.AnswerText).ToList();

                    if (correctAnswers.SequenceEqual(userProvidedAnswers))
                    {
                        // Increment score for correct answer
                        totalScore++;
                    }
                }
            }
            return totalScore;

        }
    }
}
