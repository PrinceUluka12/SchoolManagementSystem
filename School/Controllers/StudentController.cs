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
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }
        [HttpGet]
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
        [HttpPost]
        [Authorize(Roles = "STUDENT")]
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



    }
}
