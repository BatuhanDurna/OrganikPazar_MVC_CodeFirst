using OrganikPazar_Odev.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganikPazarMVC_WebApp.Models
{
    public class OrderDetails
    {
        public int ID { get; set; }


        public int User_ID { get; set; }
        [ForeignKey("User_ID")]
        public virtual Users user { get; set; }

        public int Produce_ID { get; set; }
        [ForeignKey("Produce_ID")]
        public virtual Produces produce { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public bool IsApprove { get; set; }
    }
}