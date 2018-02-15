using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Mp.Thingstatus.Models
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public virtual DbSet<Thing> Things { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<ConfigurationFile> ConfiguratonFiles {get;set;}    
        
        protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
        {
            optBuilder.UseSqlServer(@"Server=localhost\Dev;Database=Things;Trusted_Connection=True;");
        }

    }
}
