using OrganikPazar_Odev.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace OrganikPazarMVC_WebApp.Models
{
    public partial class OrganikPazarDBModel : DbContext
    {
        public OrganikPazarDBModel()
            : base("name=OrganikPazarDBModel")
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Produces> Produces { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
