using Appartment.Domain.Entities;

namespace Appartment.Domain.Repositories
{
    public interface IRequestAppartment
    {

        Task<bool> CreateRequestAsync(int apartmentId, string userId, string message);
        Task<bool> UpdateRequestAsync(int requestId, string message);
        Task<bool> DeleteRequestAsync(int requestId);
        Task<List<RequestAppartment>> GetRequestsByApartmentIdAsync(int apartmentId);
        Task<List<RequestAppartment>> GetRequestsByUserIdAsync(string userId);

        Task<List<RequestAppartment>> UpdateStatusAsync(int requestId, RequestStatus status);

        Task<List<RequestAppartment>> SendMessageToClient(MessageToClient messageToClient );
        Task<RequestAppartment?> GetRequestByIdAsync(int requestId);


    }
}
