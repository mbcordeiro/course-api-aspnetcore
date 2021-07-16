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
    public class UserControllerTest
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        private readonly HttpClient _httpClient;
        public UserControllerTest(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _httpClient = _webApplicationFactory.CreateClient();
        }

        [Fact]
        public void Login()
        {
            var loginViewModelInput = new LoginViewModelInput {
                Login = "mbcordeiro", 
                Password = "123456"
            };
            
            StringContent content = new StringContent(JsonConvert.SerializeObject(loginViewModelInput));
            var httpClienteRequest = _httpClient.PostAsync("api/v1/usuario/login", content).GetAwaiter().GetResult();
            Assert.Equal(System.Net.HttpStatusCode.OK, httpClienteRequest.StatusCode);
        }
    }
}
