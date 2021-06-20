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

namespace Course.Api.Controllers
{
    [Route("api/v1/[controller]")] 
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
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
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
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
            var courses = new List<CourseViewModelOutput>();
            courses.Add(new CourseViewModelOutput()
            {
                Login = "Teste",
                Description = "Teste",
                Name = "Teste"
            }); 
            return Ok(courses);
        }
    }
}
