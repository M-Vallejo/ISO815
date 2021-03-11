using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        internal bool IsValid() 
        {
            return !(string.IsNullOrWhiteSpace(this.Username) || string.IsNullOrWhiteSpace(Password));
        }
    }
    public class TokenViewModel
    {
        public string Token { get; set; }
    }
}
