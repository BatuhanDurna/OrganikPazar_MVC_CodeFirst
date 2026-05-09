using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganikPazar_Odev.Models
{
    public class Produces
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Ürün Adı Boş Bırakılamaz")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Bu Alan en az 5 karakter en fazla 100 karakter olmalıdır")]
        public string Name { get; set; }

        [Display(Name="Category Name")]
        public int Category_ID { get; set; }
        [ForeignKey("Category_ID")]
        public virtual Category category { get; set; }

        [Display(Name = "Brand Name")]
        public int Brand_ID { get; set; }
        [ForeignKey("Brand_ID")]
        public virtual Brands brand { get; set; }

        [Display(Name = "Unit Name")]
        public int Unit_ID { get; set; }
        [ForeignKey("Unit_ID")]
        public virtual Units unit { get; set; }

        public int Stock { get; set; }

        [Required(ErrorMessage = "Ürün Fiyatı Boş Bırakılamaz")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Ürün için açıklama zorunludur")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Picture")]
        public string ImagePath { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }


        public int? User_ID { get; set; }  

        [ForeignKey("User_ID")]
        public virtual Users user { get; set; }
    }
}