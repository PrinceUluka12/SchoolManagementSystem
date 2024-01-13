using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;
using School.Static_Data;

namespace School.Services.CourseServices
{
    public class CoursesServices : ICourseServices
    {
        private readonly AppDbContext _db;
        public CoursesServices(AppDbContext db)
        {
            _db = db;      
        }
        public async Task<bool> AssignLecturer(int lecturerId, int courseId)
        {
            var course = await _db.Courses.Include(c => c.Lecturer).FirstOrDefaultAsync(c => c.CourseId == courseId);
            var lecturer = await _db.Lecturers.FirstOrDefaultAsync(l => l.LecturerId == lecturerId);
            if (course != null && lecturer != null) 
            {
                course.Lecturer = lecturer;
                course.LecturerId = lecturerId;

                _db.Courses.Entry(course).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
           
        }

        public async Task<string> GenerateCourseReport(int courseId)
        {
            return "";
            //var course  = await _db.Courses
            //    .Include(c => c.Lecturer)
            //    .Include(c => c.Students)
            //    .ThenInclude(s => s.ex)
            //    .FirstOrDefaultAsync(c => c.CourseId == courseId);
            //string report = $"Course Report for {course.CourseName}\n";
            //report += $"Lecturer: {course.Lecturer?.FirstName}   {course.Lecturer.LastName}\n\n";
            //report += "Enrolled Students:\n";


        }

        public async Task<double> GetAverageExamResult(int courseId)
        {
            var course = await _db.Courses.Include(e => e.Exams).FirstOrDefaultAsync(c=> c.CourseId == courseId);
            if (course != null)
            {
                double averageResult = course.Exams.SelectMany(exam => exam.Results).Average(result => (int)result.Grade);
                return averageResult;
            }
            return 0;
        }

        public async Task<List<Student>> GetStudentsByGrade(int courseId, SD.Grade grade)
        {
            var course  = await _db.Courses.Include(c =>c.Students).FirstOrDefaultAsync(c => c.CourseId == courseId);
            if (course != null)
            {
                List<Student> studentsWithTargetGrade = course.Students.Where(student => _db.ExamResults.Any(result => result.StudentId == student.StudentId && result.Grade == grade)).ToList();
                return studentsWithTargetGrade;
            }
            return new List<Student>();
        }

        public async Task<List<Student>> GetStudentsInCourse(int courseId)
        {
            var course  = await _db.Courses.Include(c => c.Students).FirstOrDefaultAsync(c => c.CourseId == courseId);
            return course?.Students;
        }

        public async Task<bool> UpdateCourseInformation(Course course)
        {
            var courseToUpdate = await _db.Courses.FirstOrDefaultAsync(c =>c.CourseId == course.CourseId);
            if (courseToUpdate != null)
            {
                courseToUpdate.CourseName = course.CourseName;
                courseToUpdate.CourseType = course.CourseType;
                _db.Courses.Entry(courseToUpdate).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
