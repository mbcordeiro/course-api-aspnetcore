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

namespace Course.Api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Rota que permite autenticar um usuário cadastrado
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

        [HttpPost]
        [Route("register")]
        [CustomModelStateValidation]
        public IActionResult RegisterLogin(RegisterViewModelInput registerViewModelInput)
        {
            return Created("", registerViewModelInput);
        }
    }
}
