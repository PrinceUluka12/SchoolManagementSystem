using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;
using School.Models.DTO;
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

        public async Task<bool> AddCourse(AddCourseDTO addCourse)
        {
            try
            {
                Course newCourse = new Course()
                {
                    CourseName = addCourse.CourseName,
                };
                await _db.Courses.AddAsync(newCourse);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<List<Course>> GetAllCourse()
        {
            var courses = await _db.Courses.ToListAsync();
            return courses;
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            
            var course = await _db.Courses.FirstOrDefaultAsync(u => u.CourseId == courseId);
            if (course !=  null)
            {
                return course;
            }
            return null;
           
        }

        public async Task<List<Student>> GetStudentsInCourse(int courseId)
        {
            var allClassesInCourse = await _db.Classes.Where(a => a.CourseId == courseId).ToListAsync();
            var studentlist = new List<Student>();
            foreach (var item in allClassesInCourse)
            {
                studentlist.AddRange(item.Students);
            }
            var result = studentlist.Distinct().ToList();
            return result;
        }

        public async Task<bool> UpdateCourseInformation(Course course)
        {
            var courseToUpdate = await _db.Courses.FirstOrDefaultAsync(c =>c.CourseId == course.CourseId);
            if (courseToUpdate != null)
            {
                _db.Courses.Entry(courseToUpdate).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
