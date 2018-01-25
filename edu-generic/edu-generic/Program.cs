using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
        public DataContext() { }
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
                Location loc = new Location() { LocationId = Guid.NewGuid().ToString() , Description = "Braunschweig" };
                db.Locations.Add(loc);
                db.SaveChanges();

                //(IQueryable<Location>)
                var locs = (from b in db.Locations where b.LocationId == "szbad" select new { b.LocationId, b.Description });
                var things = (from a in db.Things select a);
                                
                foreach (var tmp in locs)
                {
                    Console.WriteLine(tmp.LocationId+ " "+tmp.Description);
                }

            }

            Console.ReadLine();
        }
    }
}
