using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;

namespace School.Services.LecturerServices
{
    public class LecturerService : ILecturerService
    {
        private readonly AppDbContext _db;

        public LecturerService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddLecturer(Lecturer lecturer)
        {
            await _db.Lecturers.AddAsync(lecturer);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Lecturer>> GetAllLecturers()
        {
            return await _db.Lecturers.ToListAsync();
        }

        public async Task<Lecturer> GetLecturerById(int lecturerId)
        {
            return await _db.Lecturers.FirstOrDefaultAsync(l => l.LecturerId == lecturerId);
        }

        public async Task<bool> RemoveLecturer(int lecturerId)
        {
            var lecturer = await _db.Lecturers.FirstOrDefaultAsync(l => l.LecturerId == lecturerId);
            if (lecturer != null)
            {
                _db.Lecturers.Remove(lecturer);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateLecturer(Lecturer lecturer)
        {
            _db.Lecturers.Entry(lecturer).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
