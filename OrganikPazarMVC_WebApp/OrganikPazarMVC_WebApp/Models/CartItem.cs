using OrganikPazar_Odev.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace OrganikPazarMVC_WebApp.Models
{
    public class CartItem
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string City { get; set; }

        public string Township { get; set; }

        public string DetailsOfAddress { get; set; }
    }
}