using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganikPazar_Odev.Models
{
    public class Units
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Birim adı zorunludur")]
        [StringLength(20, ErrorMessage = "Marka adı en fazla 20 karakter olabilir")]
        public string UnitName { get; set; }
        public bool IsActive { get; set; }
    }
}