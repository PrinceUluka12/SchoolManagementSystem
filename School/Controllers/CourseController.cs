using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Models;
using School.Models.DTO;
using School.Services.CourseServices;

namespace School.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseServices _courseServices;
        protected ResponseDto _response;
        public CourseController(ICourseServices courseServices)
        {
            _courseServices = courseServices;
            _response = new ResponseDto();
        }

       // [HttpPost("AssignLecturer")]
       //// [Authorize(Roles ="ADMIN")]
       // public async Task<IActionResult> AssignLecturer(int lecturerId, int courseId)
       // {
       //     try
       //     {
       //         var resp = await _courseServices.AssignLecturer(lecturerId, courseId);
       //         if (resp)
       //         {
       //             return Ok(_response);
       //         }
       //         else
       //         {
       //             _response.Message = "An Error Occured.....";
       //             _response.IsSuccess = false;
       //             return Ok(_response);
       //         }

       //     }
       //     catch (Exception ex )
       //     {
       //         _response.Message = ex.Message;
       //         _response.IsSuccess = false;
       //         return Ok(_response);
       //     }
       // }

        [HttpGet("GetStudentsInCourse/{courseId:int}")]
        // [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> GetStudentsInCourse(int courseId)
        {
            try
            {
                var resp = await _courseServices.GetStudentsInCourse(courseId);
                if (resp != null)
                {
                    _response.Result = resp;
                }
                else
                {
                    _response.Message = "An Error Occured.....";
                    _response.IsSuccess = false;
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return Ok(_response);
            }
        }

        [HttpPost("UpdateCourseInformation")]
        // [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> UpdateCourseInformation(Course course)
        {
            try
            {
                var resp = await _courseServices.UpdateCourseInformation(course);
                if (resp)
                {
                    return Ok(_response);
                }
                else
                {
                    _response.Message = "An Error Occured.....";
                    _response.IsSuccess = false;
                    return Ok(_response);
                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return Ok(_response);
            }
        }

        [HttpPost("AddCourse")]
        [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> AddCourse(AddCourseDTO course)
        {
            try
            {
                var resp = await _courseServices.AddCourse(course);
                if (resp)
                {
                    return Ok(_response);
                }
                else
                {
                    _response.Message = "An Error Occured.....";
                    _response.IsSuccess = false;
                    return Ok(_response);
                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return Ok(_response);
            }
        }

        [HttpGet("GetAllCourse")]
        // [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> GetAllCourse()
        {
            try
            {
                var resp = await _courseServices.GetAllCourse();
                if (resp != null)
                {
                    _response.Result = resp;
                    return Ok(_response);
                }
                else
                {
                    _response.Message = "An Error Occured.....";
                    _response.IsSuccess = false;
                    return Ok(_response);
                }

            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return Ok(_response);
            }
        }

        [HttpGet("GetCourseById/{courseId:int}")]
        // [Authorize(Roles ="ADMIN")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            try
            {
                var resp = await _courseServices.GetCourseById(courseId);
                if (resp != null)
                {
                    _response.Result = resp;
                }
                else
                {
                    _response.Message = "An Error Occured.....";
                    _response.IsSuccess = false;
                }
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
