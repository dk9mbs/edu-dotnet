using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Mp.Thingstatus.Models
{
    public class Thing
    {
        public String Id { get; set; }
        public String Description { get; set; }
        public Location Location { get; set; }
    }

    public class Location
    {
        public string LocationId { get; set; }
        public string Description { get; set; }
    }

    public class DataContext : DbContext
    {
        public DataContext() { }
        public virtual DbSet<Thing> Things { get; set; }
        public virtual DbSet<Location> Locations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(@"Server=localhost\Dev;Database=Things;Trusted_Connection=True;");
        }

    }
}