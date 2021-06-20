using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models.Users
{
    public class UserViewModelOutput
    {
        public int Code { get; set; }

        [Required(ErrorMessage = "Field '{0}' is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Field '{0}' is required")]
        public string Email { get; set; }
    }
}
