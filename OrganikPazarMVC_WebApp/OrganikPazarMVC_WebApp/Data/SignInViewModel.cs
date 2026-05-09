using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganikPazarMVC_WebApp.Data
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Mail adresi zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email gir")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}