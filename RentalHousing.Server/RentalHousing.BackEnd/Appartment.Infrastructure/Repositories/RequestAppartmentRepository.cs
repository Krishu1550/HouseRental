
using Appartment.Domain.Entities;
using Appartment.Domain.Repositories;
using Appartment.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Appartment.Infrastructure.Repositories
{
    public class RequestAppartmentRepository : IRequestAppartment
    {
        private readonly AppartmentDbContext _context;

        public RequestAppartmentRepository(AppartmentDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRequestAsync(int apartmentId, string userId, string message)
        {
            var request = new RequestAppartment
            {
                ApartmentId = apartmentId,
                UserId = userId,
                Message = message,
                RequestDate = DateTime.UtcNow,
                Status = RequestStatus.Pending
            };

            await _context.RequestAppartments.AddAsync(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRequestAsync(int requestId, string message)
        {
            var request = await _context.RequestAppartments.FindAsync(requestId);
            if (request == null) return false;

            request.Message = message;
            _context.RequestAppartments.Update(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRequestAsync(int requestId)
        {
            var request = await _context.RequestAppartments.FindAsync(requestId);
            if (request == null) return false;

            _context.RequestAppartments.Remove(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<RequestAppartment>> GetRequestsByApartmentIdAsync(int apartmentId)
        {
            return await _context.RequestAppartments
                .Include(r => r.Apartment)
                .Where(r => r.ApartmentId == apartmentId)
                .ToListAsync();
        }

        public async Task<List<RequestAppartment>> GetRequestsByUserIdAsync(string userId)
        {
            return await _context.RequestAppartments
                .Include(r => r.Apartment)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<RequestAppartment>> UpdateStatusAsync(int requestId, RequestStatus status)
        {
            var request = await _context.RequestAppartments.FindAsync(requestId);
            if (request == null) return new List<RequestAppartment>();

            request.Status = status;
            _context.RequestAppartments.Update(request);
            await _context.SaveChangesAsync();

            // Return all updated requests for the same apartment
            return await _context.RequestAppartments
                .Where(r => r.ApartmentId == request.ApartmentId)
                .ToListAsync();
        }

        public async Task<List<RequestAppartment>> SendMessageToClient(MessageToClient messageToClient)
        {
            var request = await _context.RequestAppartments.FindAsync(messageToClient.RequestId);
            if (request == null) return new List<RequestAppartment>();

            request.Message = messageToClient.Message;
            _context.RequestAppartments.Update(request);
            await _context.SaveChangesAsync();

            return await _context.RequestAppartments
                .Where(r => r.UserId == request.UserId)
                .ToListAsync();
        }

        public async Task<RequestAppartment?> GetRequestByIdAsync(int requestId)
        {
            return await _context.RequestAppartments
                .Include(r => r.Apartment)
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }
    }
}
