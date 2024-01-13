using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;
using School.Models.DTO;
using School.Models.DTO.IdentityDTO;
using School.Services.IdentityService;
using School.Static_Data;
using System;
using static School.Static_Data.SD;

namespace School.Services.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _db;
        private static readonly Random random = new Random();
        private readonly IIdentityService _identity;
        public StudentService(AppDbContext db, IIdentityService identity)
        {
            _db = db;
            _identity = identity;
        }

        public async Task<bool> AddStudent(AddStudentDTO student)
        {
            Student newStudent = new Student()
            {
                FirstName = student.FirstName,
                MiddleName = student.LastName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                ClassId = student.ClassId,
                MatricNo = GenerateRandomNumber()
            };
            await _db.Students.AddAsync(newStudent);
            await _db.SaveChangesAsync();

            RegistrationRequestDTO registration = new RegistrationRequestDTO()
            {
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                MatricNo = newStudent.MatricNo,
                Password = GeneratePassword(newStudent.LastName, newStudent.MatricNo) + "@",
                Email = newStudent.FirstName + "." + newStudent.LastName + "@gmail.com"
               
            };
            var loginReg = await _identity.Register(registration);


            return true;
        }

        public async Task<double> CalculateGPA(int StudentId)
        {
            var gradeValues = Enum.GetValues(typeof(Grade)).Cast<int>().ToList();
            var examResults = await _db.ExamResults.Where(result => result.Student.StudentId == StudentId).ToListAsync();
            if (examResults.Any())
            {
                var totalPoints = 0;
                var totalCredits = 0;

                foreach (var result in examResults)
                {
                    var gradeValue = gradeValues[(int)result.Grade];
                    var courseCredits = result.Exam.Course.Credits; 

                    totalPoints += gradeValue * courseCredits;
                    totalCredits += courseCredits;
                }

                return (double)totalPoints / totalCredits;
            }
            else
            {
                return 0.0;
            }
        }

        public async Task<bool> EnrollInCourse(int courseId, int StudentId)
        {
            try
            {
                var student  = await _db.Students.Include(s => s.Courses).FirstOrDefaultAsync(s => s.StudentId == StudentId);
                var courseData = await _db.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);

                if (student != null && courseData != null)
                {
                    student.Courses.Add(courseData);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<string> GenerateTranscript(int studentId)
        {
            var studentWithCourses = await _db.Students.Include(s => s.Courses)
                .ThenInclude(c => c.Exams)
                .ThenInclude(e => e.Results)
                .ThenInclude(r => r.Exam)
                .FirstOrDefaultAsync(s => s.StudentId ==  studentId);
            if (studentWithCourses == null )
            {
                return "Student not found.";
            }
            else
            {
                var transcript  = $"Transcript for {studentWithCourses.FirstName}    {studentWithCourses.LastName}\n\n";
                foreach (var course in studentWithCourses.Courses)
                {
                    transcript += $"Course: {course.CourseName}\n";
                    foreach (var result in course.Exams.SelectMany(e => e.Results.Where(r => r.Student.StudentId == studentId)))
                    {
                        transcript += $"  Exam: {result.Exam.ExamName}, Grade: {result.Grade}\n";
                    }
                    transcript += "\n";
                }
                return transcript;
            }
        }

        public async Task<Dictionary<DateTime, bool>> GetAttendanceReport(int studentId)
        {
            var attendanceReport = new Dictionary<DateTime, bool>();
            var student = await _db.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
            if (student != null)
            {
                foreach (var record in student.AttendanceRecords)
                {
                    attendanceReport.Add(record.Date, record.IsPresent);
                }
                return attendanceReport;
            }
            return null;

        }

        public async Task<List<Course>> GetClassSchedule(int StudentId)
        {
            var student = await _db.Students.Include(s => s.Class).ThenInclude(c => c.Courses).FirstOrDefaultAsync(s => s.StudentId == StudentId);
            return student?.Class?.Courses;
        }

        public async Task<List<Course>> GetEnrolledCourses(int StudentId)
        {
            try
            {
                var  student = await _db.Students.Include(s => s.Courses).FirstOrDefaultAsync(s => s.StudentId == StudentId);
                if (student != null)
                {
                    List<Course> enrolledCourses = student.Courses;
                    return enrolledCourses;
                }
                else
                {
                    return new List<Course>();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<SD.Grade> GetExamGrade(int examId, int studentId)
        {
            try
            {
                var result = await _db.ExamResults
                    .FirstOrDefaultAsync(er => er.Student.StudentId == studentId && er.Exam.ExamId == examId);
                return result.Grade;
            }
            catch (Exception)
            {

                return SD.Grade.F;
            }
        }

        public async Task<List<ExamResult>> GetExamResults(int studentId)
        {
            try
            {
                var result  =  await _db.Students.Where(s => s.StudentId == studentId)
                    .SelectMany(s => s.Courses)
                    .SelectMany(c => c.Exams)
                    .SelectMany(e => e.Results)
                    .ToListAsync();
                return result;
            }
            catch (Exception)
            {

                return new List<ExamResult>();
            }
        }

        public async Task<Student> GetStudentInformation(int StudentId)
        {
            var student = await _db.Students
                 .Include(s => s.Courses)
                 .FirstOrDefaultAsync(s => s.StudentId == StudentId);
            return student;
        }

        public async Task<List<Student>> GetStudents()
        {
            var data = await _db.Students.ToListAsync();
            return data;
        }

        public async Task<bool> PayTuitionFees(int studentId, double amount)
        {
            TuitionPayment payment = new TuitionPayment
            {
                StudentId = studentId,
                Amount = amount,
                PaymentDate = DateTime.Now
            };

            await _db.TuitionPayments.AddAsync(payment);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RequestLeaveOfAbsence(int studentId, DateTime startDate, DateTime endDate)
        {
            var student = await _db.Students.FindAsync(studentId);
            var leaveOfAbsence = new LeaveOfAbsence
            {
                Student = student,
                StartDate = startDate,
                EndDate = endDate
            };
            await _db.LeaveOfAbsences.AddAsync(leaveOfAbsence);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStudentName(int StudentId, string newFirstName = "", string newMiddleName = "", string newLastName = "")
        {
            var student = await _db.Students.AsNoTracking().FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (student != null)
            {
                if (!string.IsNullOrEmpty(newFirstName))
                {
                    student.FirstName = newFirstName;
                }

                if (!string.IsNullOrEmpty(newMiddleName))
                {
                    student.MiddleName = newMiddleName;
                }

                if (!string.IsNullOrEmpty(newLastName))
                {
                    student.LastName = newLastName;
                }
                _db.Students.Entry(student).State = EntityState.Modified;

                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> WithdrawFromCourse(int courseId, int StudentId)
        {
            try
            {
                var course = await _db.Courses.FindAsync(courseId);
                if (course != null)
                {
                    _db.Courses.Remove(course); 
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        private  static string GenerateRandomNumber()
        {
            int currentYear = DateTime.Now.Year % 100;
            string yearDigits = currentYear.ToString("D2");
            string randomDigits = GenerateRandomDigits(4);
            string result = $"{yearDigits}{randomDigits}";
            return result;
        }

        private static string GenerateRandomDigits(int length)
        {
            char[] digits = new char[length];
            for (int i = 0; i < length; i++)
            {
                digits[i] = (char)('0' + random.Next(10));
            }
            return new string(digits);
        }

        private  static string GeneratePassword(string lastName, string matriculationNumber)
        {
            string lastNamePrefix = lastName.Length >= 2 ? lastName.Substring(0, 2) : lastName;

            string numericMatriculationNumber = new string(matriculationNumber.Where(char.IsDigit).ToArray());

            numericMatriculationNumber = numericMatriculationNumber.Length > 6
                ? numericMatriculationNumber.Substring(0, 6)
                : numericMatriculationNumber;

            string generatedPassword = $"{lastNamePrefix}{numericMatriculationNumber}";
            generatedPassword = generatedPassword.PadRight(8, ' ').Substring(0, 8);
            generatedPassword = char.ToUpper(generatedPassword[0]) + generatedPassword.Substring(1);

            return generatedPassword;
        }

    }
}
