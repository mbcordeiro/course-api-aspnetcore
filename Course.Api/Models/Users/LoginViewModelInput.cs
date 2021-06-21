using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models.Users
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "Login is required")]
        public string login { get; set; }

        [Required(ErrorMessage = "E-mail is required")]
        public string password { get; set; }
    }
}
