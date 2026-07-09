using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganikPazarMVC_WebApp.Models
{
    public class AddressViewModel
    {
        public int City_ID { get; set; }
        [ForeignKey("City_ID")]
        public virtual City city { get; set; }

        public int District_ID { get; set; }
        [ForeignKey("District_ID")]
        public virtual District district { get; set; }


        [Required(ErrorMessage = "Bu alan boş bıraklamaz")]
        public string ReceiverName { get; set; }

        [Required(ErrorMessage = "Bu alan boş bıraklamaz")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bu alan boş bıraklamaz")]
        public string AddresDetail { get; set; }
    }
}