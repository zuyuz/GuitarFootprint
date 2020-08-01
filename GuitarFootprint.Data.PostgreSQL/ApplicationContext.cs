using System;
using GuitarFootprint.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace GuitarFootprint.Data.PostgreSQL
{
    public class ApplicationContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AudioConfiguration());
        }
    }
}
