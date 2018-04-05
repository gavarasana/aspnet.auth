using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ravi.learn.identity.web.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "User name is needed")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is needed")]
        public string Password { get; set; }
    }
}
