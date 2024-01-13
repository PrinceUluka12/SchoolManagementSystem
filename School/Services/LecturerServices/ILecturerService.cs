using School.Models;

namespace School.Services.LecturerServices
{
    public interface ILecturerService
    {
        Task<bool> AddLecturer(Lecturer lecturer);
        Task<bool> UpdateLecturer(Lecturer lecturer);
        Task<bool> RemoveLecturer(int lecturerId);
        Task<Lecturer> GetLecturerById(int lecturerId);
        Task<List<Lecturer>> GetAllLecturers();
    }
}
