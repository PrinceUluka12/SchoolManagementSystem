using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using School.Extensions;
using School.Models;
using School.Models.DTO;
using School.Services.StudentServices;

namespace School.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        protected ResponseDto _response;

        public StudentController(IStudentService service)
        {
            _service = service;
            _response = new ResponseDto();
        }
        [HttpGet("GetAllStudents")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var data = await _service.GetStudents();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost("AddStudent")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddStudent(AddStudentDTO student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.AddStudent(student);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpPost("EnrollInCourse")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> EnrollInCourse(int courseId, int StudentId)
        {
            try
            {
                var resp = await _service.EnrollInCourse(courseId, StudentId);
                if (resp)
                {
                    return Ok(_response);
                }
                _response.Message = "AN ERROR OCCURED";
                _response.IsSuccess = false;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return Ok(_response);
            }
        }



    }
}
