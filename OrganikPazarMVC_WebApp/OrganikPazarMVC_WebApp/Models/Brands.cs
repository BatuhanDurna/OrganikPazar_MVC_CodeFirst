using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganikPazar_Odev.Models
{
    public class Brands
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Marka adı zorunludur")]
        [StringLength(50,ErrorMessage = "Marka adı en fazla 50 karakter olabilir")]
        public string BrandName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Produces> Produces { get; set; }
    }
}