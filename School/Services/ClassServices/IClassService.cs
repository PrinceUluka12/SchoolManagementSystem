using School.Models;
using School.Static_Data;

namespace School.Services.ClassServices
{
    public interface IClassService
    {
        Task<bool> AssignLecturer(int lecturerId, int classId);
        Task<bool> UpdateClassInformation(Class data);
        Task<List<Student>> GetStudentsInClass(int classId);
        Task<List<Student>> GetStudentsByGrade(int classId, SD.Grade grade);
        Task<Class> GetClassById(int classId);
    }
}
