using School.Models;
using School.Models.DTO;
using static School.Static_Data.SD;

namespace School.Services.StudentServices
{
    public interface IStudentService
    {
        Task<bool> EnrollInCourse(int courseId, int StudentId);
        Task<bool> AddStudent(AddStudentDTO student);
        Task<bool> WithdrawFromCourse(int courseId, int StudentId);
        Task<bool> UpdateStudentName(int StudentId, string newFirstName ="", string newMiddleName ="", string newLastName ="");
        Task<double> CalculateGPA(int StudentId);
        Task<Grade> GetExamGrade(int examId, int StudentId);
        Task<List<ExamResult>> GetExamResults(int StudentId);
        Task<Student> GetStudentInformation(int StudentId);
        Task<List<Class>> GetEnrolledClass(int StudentId);
        Task<List<Class>> GetClassSchedule(int StudentId);
        Task<Dictionary<DateTime, bool>> GetAttendanceReport(int studentId);
        Task<string> GenerateTranscript(int studentId);

        Task<bool> PayTuitionFees(int studentId, double amount);
        Task<bool> RequestLeaveOfAbsence(int studentId, DateTime startDate, DateTime endDate);
        Task<List<Student>> GetStudents();
    }
}
