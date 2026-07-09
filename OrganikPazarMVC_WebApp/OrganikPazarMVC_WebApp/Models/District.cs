using OrganikPazar_Odev.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OrganikPazarMVC_WebApp.Models
{
    public class District
    {
        public int ID { get; set; }
        public int City_ID { get; set; }
        [ForeignKey("City_ID")]
        public virtual City city { get; set; }
        public string DistrictName { get; set; }
        
    }
}