using Appartment.Application.DTOs;

namespace Appartment.Application.Interface
{
    public interface IRequestApartmentService
    {

        Task<bool> CreateRequestAsync(CreateRequestApartmentDto dto);
        Task<bool> UpdateRequestAsync(UpdateRequestApartmentDto dto);
        Task<bool> DeleteRequestAsync(int requestId);
        Task<IEnumerable<RequestApartmentDto>> GetRequestsByApartmentIdAsync(int apartmentId);
        Task<IEnumerable<RequestApartmentDto>> GetRequestsByUserIdAsync(string userId);
        Task<RequestApartmentDto?> GetRequestByIdAsync(int requestId);
        Task<bool> UpdateStatusAsync(int requestId, string status);
    }
}
