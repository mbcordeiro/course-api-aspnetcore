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
using Course.Api.Business.Repositories;
using Course.Api.Services;

namespace Course.Api.Controllers
{
    /// <summary>
    /// Users controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        UsersController(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

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
            var user = _userRepository.GetUser(loginViewModelInput.Login);

            if(user == null)
            {
                return BadRequest("There was an error trying to find the user");
            }

            var userViewModelOutput = new UserViewModelOutput
            {
                Code = user.Code,
                Login = user.Login,
                Email = user.Email
            };

            var token = _authenticationService.GenerateToken(userViewModelOutput);

            return Ok(new { 
                Token = token,
                User = userViewModelOutput
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
            var user = new User{ 
                Login = registerViewModelInput.Login,
                Password = registerViewModelInput.Email,
                Email = registerViewModelInput.Password
            };
            
            _userRepository.Add(user);
            _userRepository.Save();
            return Created("", registerViewModelInput);
        }
    }
}
