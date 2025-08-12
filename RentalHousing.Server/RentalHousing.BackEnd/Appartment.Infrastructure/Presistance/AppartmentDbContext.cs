using Appartment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appartment.Infrastructure.Presistance
{
    public class AppartmentDbContext : DbContext
    {
        public AppartmentDbContext(DbContextOptions<AppartmentDbContext> options) : base(options)
        {
        }

        public DbSet<RequestAppartment> RequestAppartments { get; set; }
        public DbSet<Apartment> Apartments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure owned entity
            modelBuilder.Entity<Apartment>()
                .OwnsOne(a => a.Address);

            // Seed Apartments
            modelBuilder.Entity<Apartment>().HasData(
                new Apartment
                {
                    Id = 1,
                    Title = "Modern 2-Bedroom Apartment",
                    Description = "Spacious apartment with balcony and great city view.",
                    Price = 1200,
                    Bedrooms = 2,
                    Bathrooms = 1,
                    City = "Paris",
                    OwnerId = "user-123"
                },
                new Apartment
                {
                    Id = 2,
                    Title = "Cozy Studio",
                    Description = "Small but comfortable studio near the metro.",
                    Price = 750,
                    Bedrooms = 1,
                    Bathrooms = 1,
                    City = "Lyon",
                    OwnerId = "user-456"
                }
            );

            // Seed Address (owned entity data is seeded separately)
            modelBuilder.Entity<Apartment>().OwnsOne(a => a.Address).HasData(
                new
                {
                    ApartmentId = 1,
                    Street = "123 Main Street",
                    City = "Paris",
                    State = "Île-de-France",
                    PostalCode = "75001",
                    Country = "France"
                },
                new
                {
                    ApartmentId = 2,
                    Street = "45 Rue de Lyon",
                    City = "Lyon",
                    State = "Auvergne-Rhône-Alpes",
                    PostalCode = "69001",
                    Country = "France"
                }
            );

           
        }
    }
}
