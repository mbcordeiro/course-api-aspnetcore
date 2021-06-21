using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Api.Models.Courses;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Course.Api.Business.Repositories;
using Course.Api.Business.Entities;

namespace Course.Api.Controllers
{
    [Route("api/v1/[controller]")] 
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// This service allows created courses for an authenticated user
        /// </summary>
        /// <param name="cursoViewModelInput"></param>
        /// <returns>Return 201 status and user course data</returns>
        [SwaggerResponse(statusCode: 201, description: "Success when created a course", Type = typeof(CourseViewModelInput))]
        [SwaggerResponse(statusCode: 401, description: "Not authorized")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateCourse(CourseViewModelInput courseViewModelInput)
        {
            var userCode = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var course = new CourseEntity {
                Name = courseViewModelInput.Name,
                Description = courseViewModelInput.Description,
                UserCode = userCode
            };
            _courseRepository.Add(course);
            _courseRepository.Save();
            return Created("", courseViewModelInput);
        }

        /// <summary>
        /// This service allows you to obtain courses 
        /// </summary>       
        /// <returns>Return 201 User Course Status and Data</returns>
        [SwaggerResponse(statusCode: 201, description: "Success when get a course", Type = typeof(CourseViewModelOutput))]
        [SwaggerResponse(statusCode: 401, description: "Not authorized")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCourses()
        {
            var userCode = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var courses = new List<CourseViewModelOutput>();
            _courseRepository.GetByUser(userCode)
                .Select(s => new CourseViewModelOutput
                {
                    Login = s.User.Login,
                    Name = s.Name,
                    Description = s.Description
                }); 
            return Ok(courses);
        }
    }
}
