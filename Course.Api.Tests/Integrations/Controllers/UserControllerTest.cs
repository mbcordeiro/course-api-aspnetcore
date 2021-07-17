using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Course.Api.Models.Users;
using Newtonsoft.Json;
using Xunit;

namespace Course.Api.Tests.Integrations.Controllers
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        private readonly HttpClient _httpClient;
        public UserControllerTest(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _httpClient = _webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task Login()
        {
            //Arrange
            var loginViewModelInput = new LoginViewModelInput {
                Login = "mbcordeiro", 
                Password = "123456"
            }; 
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginViewModelInput), Encoding.UTF8, "application/json");

            //Act
            var httpClienteRequest = await _httpClient.PostAsync("api/v1/usuario/login", content);
            
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, httpClienteRequest.StatusCode);
        }

        [Fact]
        public void Register()  
        {
            //Arrange
            var registerViewModelInput = new RegisterViewModelInput
            {
                Email = "matheusdebarroscordeiro@gmail.com",
                Login = "mbcordeiro",
                Password = "123456"
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(registerViewModelInput), Encoding.UTF8, "application/json");

            //Act
            var httpClienteRequest = _httpClient.PostAsync("api/v1/usuario/register", content).GetAwaiter().GetResult();
           
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, httpClienteRequest.StatusCode);
        }

        
    }
}
