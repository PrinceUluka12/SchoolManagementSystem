using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;
using School.Static_Data;

namespace School.Services.ClassServices
{
    public class ClassService : IClassService
    {
        private readonly AppDbContext _db;
        public ClassService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> AssignLecturer(int lecturerId, int classId)
        {
            var existingClass = await _db.Classes.Include(c => c.Lecturer).FirstOrDefaultAsync(c => c.ClassId == classId);
            var lecturer = await _db.Lecturers.FirstOrDefaultAsync(l => l.LecturerId == lecturerId);
            if (existingClass != null && lecturer != null)
            {
                existingClass.Lecturer = lecturer;
                existingClass.LecturerId = lecturerId;

                _db.Classes.Entry(existingClass).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Class> GetClassById(int classId)
        {
            var data = await _db.Classes.FirstOrDefaultAsync(u => u.ClassId == classId);
            if (data != null)
            {
                return data;
            }
            return null;
        }

        public async Task<List<Student>> GetStudentsByGrade(int classId, SD.Grade grade)
        {
            var data = await _db.Classes.Include(c => c.Students).FirstOrDefaultAsync(c => c.ClassId == classId);
            if (data != null)
            {
                List<Student> studentsWithTargetGrade = data.Students.Where(student => _db.ExamResults.Any(result => result.StudentId == student.StudentId && result.Grade == grade)).ToList();
                return studentsWithTargetGrade;
            }
            return new List<Student>();
        }

        public async Task<List<Student>> GetStudentsInClass(int classId)
        {
            var data = await _db.Classes.Include(c => c.Students).FirstOrDefaultAsync(c => c.ClassId == classId);
            return data?.Students;
        }

        public async Task<bool> UpdateClassInformation(Class data)
        {

            var classToUpdate = await _db.Classes.FirstOrDefaultAsync(c => c.ClassId == data.ClassId);
            if (classToUpdate != null)
            {
                _db.Classes.Entry(classToUpdate).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
