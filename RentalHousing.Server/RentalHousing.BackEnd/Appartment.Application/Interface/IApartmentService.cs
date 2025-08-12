using Appartment.Application.DTOs;

namespace Appartment.Application.Interface
{
    public interface IApartmentService
    {
        Task<ApartmentDto> AddApartmentAsync(CreateApartmentDto dto);
        Task<ApartmentDto?> GetByIdAsync(int id);
        Task<IEnumerable<ApartmentDto>> GetAllAsync();
        Task<IEnumerable<ApartmentDto>> GetByOwnerIdAsync(string ownerId);
        Task<IEnumerable<ApartmentDto>> SearchAsync(string city, decimal? minPrice, decimal? maxPrice, int? bedrooms);
        Task UpdateApartmentAsync(UpdateApartmentDto dto);
        Task DeleteApartmentAsync(int id);

    }
}
