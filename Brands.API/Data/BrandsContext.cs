using Microsoft.EntityFrameworkCore;
using Brands.API.Models;
using System.Security.Cryptography.X509Certificates;

namespace Brands.Data
{
    public class BrandsContext: DbContext
    {
        public BrandsContext(DbContextOptions<BrandsContext> options) : base(options) { }
        // Define the Brands DbSet to represent the Brands entity in the database
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Write seed data for brands
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Toyota", Country = "Japon", CreatedAtYear = 1937 },
                new Brand { Id = 2, Name = "Ford", Country = "Estados Unidos", CreatedAtYear = 1903 },
                new Brand { Id = 3, Name = "BMW", Country = "Alemania", CreatedAtYear = 1916 }
            );
        }
    }
}
