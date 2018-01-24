using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace edu.csharp.generic
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
        public virtual DbSet<Thing> Things { get; set; }
        public virtual DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(@"Server=localhost\Dev;Database=Things;Trusted_Connection=True;");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            using (var db = new DataContext())
            {
                Location loc = new Location() { LocationId = "szbad", Description = "Braunschweig" };
                db.Locations.Add(loc);
                //context.SaveChanges();
                
                //var test = context.Locations.To
                var test = (from b in db.Locations select b);
            }

            Console.WriteLine("T");
            Console.ReadLine();
        }
    }
}
