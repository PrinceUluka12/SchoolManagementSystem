using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;

namespace School.Services.ExamSservices
{
    public class ExamService : IExamService
    {
        private readonly AppDbContext _db;
        public ExamService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AddExam(Exam exam)
        {
            await _db.AddAsync(exam);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExam(int examId)
        {
            var exam = await _db.Exams.FirstOrDefaultAsync(e => e.ExamId == examId);
            if (exam != null)
            {
                _db.Exams.Remove(exam);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async  Task<List<Exam>> GetAllExams()
        {
          return await _db.Exams.Include(e => e.Questions).ThenInclude(q => q.Options).ToListAsync(); 
        }

        public async Task<Exam> GetExamById(int examId)
        {
            var exam = await _db.Exams.Include(e => e.Questions).ThenInclude(q => q.Options).FirstOrDefaultAsync(e => e.ExamId == examId);
            return exam;
        }

        public async Task<List<Exam>> GetExamsForClass(int classId)
        {
           var data = await _db.Exams.Where(e => e.classes.ClassId == classId).ToListAsync();
            return data;
        }

        public async Task<bool> UpdateExam(Exam exam)
        {
            _db.Exams.Entry(exam).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
