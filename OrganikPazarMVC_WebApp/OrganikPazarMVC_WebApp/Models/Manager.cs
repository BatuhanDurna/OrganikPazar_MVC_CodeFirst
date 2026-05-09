using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganikPazar_Odev.Models
{
    public class Manager
    {
        public int ID { get; set; }

        [StringLength(maximumLength:50, ErrorMessage = "Bu alan 50 karakter olmalıdır")]
        public string Name { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "Bu alan 50 karakter olmalıdır")]
        public string Surname { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "En fazla 150 karakter en az 6 karakter olmalıdır")]
        public string Mail { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [StringLength(300, MinimumLength = 8, ErrorMessage = "En az 8 karakter olmalıdır")]
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}