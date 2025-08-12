using Appartment.Domain.Entities;
using Appartment.Domain.Repositories;
using Appartment.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Appartment.Infrastructure.Repositories
{
    public class AppartmentRepository : IAppartmentRepository
    {
        private readonly AppartmentDbContext _context;

        public AppartmentRepository(AppartmentDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Apartment> AddAsync(Apartment apartment)
        {
            await _context.Apartments.AddAsync(apartment);
            await _context.SaveChangesAsync();
            return apartment;
        }

        // Read by Id
        public async Task<Apartment?> GetByIdAsync(int id)
        {
            return await _context.Apartments
                // If you have an Owner navigation property
                .Include(a => a.Address) // If Address is a separate class
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Read all
        public async Task<IEnumerable<Apartment>> GetAllAsync()
        {
            return await _context.Apartments
                
                .Include(a => a.Address)
                .ToListAsync();
        }

        // Read by owner Id
        public async Task<IEnumerable<Apartment>> GetByOwnerIdAsync(string ownerId)
        {
            return await _context.Apartments
                .Include(a => a.Address)
                .Where(a => a.OwnerId == ownerId)
                .ToListAsync();
        }

        // Search with filters
        public async Task<IEnumerable<Apartment>> SearchAsync(string city, decimal? minPrice, decimal? maxPrice, int? bedrooms)
        {
            var query = _context.Apartments
                .Include(a => a.Address)
                .AsQueryable();

            if (!string.IsNullOrEmpty(city))
                query = query.Where(a => a.Address.City.ToLower().Contains(city.ToLower()));

            if (minPrice.HasValue)
                query = query.Where(a => a.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(a => a.Price <= maxPrice.Value);

            if (bedrooms.HasValue)
                query = query.Where(a => a.Bedrooms == bedrooms.Value);

            return await query.ToListAsync();
        }

        // Update
        public async Task UpdateAsync(Apartment apartment)
        {
            _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment != null)
            {
                _context.Apartments.Remove(apartment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
