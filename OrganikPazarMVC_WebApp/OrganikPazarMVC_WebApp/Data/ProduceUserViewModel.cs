using OrganikPazar_Odev.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganikPazarMVC_WebApp.Data
{
    public class ProduceUserViewModel
    {
        public int Produce_ID { get; set; }
        [ForeignKey("Produce_ID")]
        public virtual Produces produce { get; set; }

        public string ProduceName { get; set; }
        public decimal Price { get; set; }

        public int Category_ID { get; set; }
        [ForeignKey("Category_ID")]
        public virtual Category category { get; set; }
        public string CategoryName { get; set; }

        public string ImagePath { get; set; }

        public int? User_ID { get; set; }
        [ForeignKey("User_ID")]
        public virtual Users user { get; set; }

        public DateTime CreationTime { get; set; }


    }
}