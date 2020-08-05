using System;
using GuitarFootprint.Data.Entities;
using GuitarFootprint.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace GuitarFootprint.Data.PostgreSQL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Audio> Audio { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AudioConfiguration());
        }
    }
}
