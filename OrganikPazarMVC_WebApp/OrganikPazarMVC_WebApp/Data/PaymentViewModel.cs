using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganikPazarMVC_WebApp.Data
{
    public class PaymentViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Cardname { get; set; }

        [Required(ErrorMessage = "*")]
        public string Cardnumber { get; set; }

        [Required(ErrorMessage = "*")]
        public string Expiry { get; set; }

        [Required(ErrorMessage = "*")]
        public string CVV { get; set; }
    }
}