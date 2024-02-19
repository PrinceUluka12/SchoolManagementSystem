using School.Models;
using School.Models.DTO;
using School.Static_Data;

namespace School.Services.CourseServices
{
    public interface ICourseServices
    {
        Task<bool> AddCourse(AddCourseDTO addCourse);
        Task<bool> UpdateCourseInformation(Course course);
        Task<List<Student>> GetStudentsInCourse(int courseId);
        Task<Course> GetCourseById(int courseId);
        Task<List<Course>> GetAllCourse();
    }
}
