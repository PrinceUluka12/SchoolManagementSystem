using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;

namespace School.Services.QuestionServices
{
    public class QuestionService : IQuestionService
    {
        private readonly AppDbContext _db;
        public QuestionService(AppDbContext db)
        {
                _db = db;
        }
        public async Task<bool> AddQuestion(Question question)
        {
           await  _db.Questions.AddAsync(question);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteQuestion(int questionId)
        {
            var question = await _db.Questions.FirstOrDefaultAsync(q => q.QuestionId == questionId);
            if (question != null)
            {
                _db.Questions.Remove(question);
                _db.SaveChanges();
               
            }
            return true;
        }

        public async Task<Question> GetQuestionById(int questionId)
        {
            var question = await _db.Questions.Include(a => a.Options).FirstOrDefaultAsync(q => q.QuestionId == questionId);
            return question;
        }

        public async Task<List<Question>> GetQuestionsByExamId(int examId)
        {
            var question = await _db.Questions.Include(a => a.Options).Where(q => q.ExamId == examId).ToListAsync();
            return question;
        }

        public async Task<bool> UpdateQuestion(Question question)
        {
            _db.Questions.Entry(question).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
