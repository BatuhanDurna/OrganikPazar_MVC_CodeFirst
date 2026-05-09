using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganikPazar_Odev.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Kategori Adı zorunludur")]
        [StringLength(maximumLength:50, ErrorMessage = "En fazla 50 karakter yazılabilir")]
        public string CategoryName { get; set; }

        [Display(Name = "Aktif mi")]
        public bool IsActive { get; set; }

        public virtual ICollection<Produces> Produces { get; set; }
    }
}