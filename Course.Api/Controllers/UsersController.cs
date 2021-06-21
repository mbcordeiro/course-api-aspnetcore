using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Api.Models.Users;
using Course.Api.Models;
using Swashbuckle.AspNetCore.Annotations;
using Course.Api.Filters;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Course.Api.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Course.Api.Business.Entities;

namespace Course.Api.Controllers
{
    /// <summary>
    /// Users controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Route that allows to authenticate a registered user
        /// </summary>      
        /// <returns>Returns user and token on success</returns>
        [SwaggerResponse(statusCode: 200, description: "Authenticate success", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Required fields", Type = typeof(ValidateFieldViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Internal server error", Type = typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("login")]
        [CustomModelStateValidation]
        public IActionResult Login(LoginViewModelInput loginViewModelInput)
        {
            var user = new UserViewModelOutput
            {
                Code = 1,
                Login = "mbcordeiro",
                Email = "mbcordeiro@email.com"
            };

            var secret = Encoding.ASCII.GetBytes("CVbniusdfhbasdlk&*6@gslkdjfasjd-sdflsiuçdof213553||");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Code.ToString()),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerate = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerate);

            return Ok(new { 
                Token = token,
                User = user
            });
        }

        /// <summary>
        /// Route that allows to register a user
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]     
        [CustomModelStateValidation]
        public IActionResult RegisterLoginAsync(RegisterViewModelInput registerViewModelInput)
        {
            var options = new DbContextOptionsBuilder<CourseDbContext>();
            options.UseSqlServer("server = localhost, database = courses; user=sa; password=sa;");
            var context = new CourseDbContext(options.Options);

            var pendenciesMigrations = context.Database.GetPendingMigrations();

            if(pendenciesMigrations.Count() > 0)
            {
                context.Database.Migrate();
            }

            var user = new User();
            user.Login = registerViewModelInput.Login;
            user.Email = registerViewModelInput.Email;
            user.Password = registerViewModelInput.Password;

            context.Users.Add(user);
            context.SaveChanges();
            return Created("", registerViewModelInput);
        }
    }
}
