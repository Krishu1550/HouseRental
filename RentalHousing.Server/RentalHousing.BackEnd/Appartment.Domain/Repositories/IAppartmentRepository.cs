using Appartment.Domain.Entities;

namespace Appartment.Domain.Repositories
{
    public interface IAppartmentRepository
    {
        // Create
        Task<Apartment> AddAsync(Apartment apartment);

        // Read
        Task<Apartment?> GetByIdAsync(int id);
        Task<IEnumerable<Apartment>> GetAllAsync();
        Task<IEnumerable<Apartment>> GetByOwnerIdAsync(string ownerId);
        Task<IEnumerable<Apartment>> SearchAsync(string city, decimal? minPrice, decimal? maxPrice, int? bedrooms);

        // Update
        Task UpdateAsync(Apartment apartment);

        // Delete
        Task DeleteAsync(int id);

      
    }
}
