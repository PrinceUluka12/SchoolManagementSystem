using School.Models;
using School.Static_Data;

namespace School.Services.CourseServices
{
    public interface ICourseServices
    {
        Task<bool> AssignLecturer(int lecturerId, int courseId);
        Task<double> GetAverageExamResult(int courseId);
        Task<bool> UpdateCourseInformation(Course course);
        Task<string> GenerateCourseReport(int courseId);
        Task<List<Student>> GetStudentsInCourse(int courseId);
        Task<List<Student>> GetStudentsByGrade(int courseId , SD.Grade grade);
    }
}
