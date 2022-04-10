using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Models;

namespace BerrasBio.Data
{
    public class BerrasBioContext : DbContext
    {
        public BerrasBioContext (DbContextOptions<BerrasBioContext> options)
            : base(options)
        {
        }

        public DbSet<BerrasBio.Models.Display> Display { get; set; }
        public DbSet<BerrasBio.Models.Movie> Movies { get; set;}
        public DbSet<BerrasBio.Models.Salon> Salons { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movie");
            modelBuilder.Entity<Salon>().ToTable("Salon");
            modelBuilder.Entity<Display>().ToTable("Display");
        }
    }
}
