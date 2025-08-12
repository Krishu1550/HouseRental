using Appartment.Application.DTOs;
using Appartment.Application.Interface;
using Appartment.Domain.Entities;
using Appartment.Domain.Repositories;
using AutoMapper;

namespace Appartment.Application.Services
{
    public class RequestApartmentService : IRequestApartmentService
    {
        private readonly IRequestAppartment _repository;
        private readonly IMapper _mapper;

        public RequestApartmentService(IRequestAppartment repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateRequestAsync(CreateRequestApartmentDto dto)
        {
            return await _repository.CreateRequestAsync(dto.ApartmentId, dto.UserId, dto.Message);
        }

        public async Task<bool> DeleteRequestAsync(int requestId)
        {
            return await _repository.DeleteRequestAsync(requestId);
        }

        public async Task<IEnumerable<RequestApartmentDto>> GetRequestsByApartmentIdAsync(int apartmentId)
        {
            var requests = await _repository.GetRequestsByApartmentIdAsync(apartmentId);
            return _mapper.Map<IEnumerable<RequestApartmentDto>>(requests);
        }

        public async Task<IEnumerable<RequestApartmentDto>> GetRequestsByUserIdAsync(string userId)
        {
            var requests = await _repository.GetRequestsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<RequestApartmentDto>>(requests);
        }

        public async Task<RequestApartmentDto?> GetRequestByIdAsync(int requestId)
        {
            var request = await _repository.GetRequestByIdAsync(requestId);
            return request == null ? null : _mapper.Map<RequestApartmentDto>(request);
        }

        public async Task<bool> UpdateRequestAsync(UpdateRequestApartmentDto dto)
        {
            return await _repository.UpdateRequestAsync(dto.Id, dto.Message);
        }

        public async Task<bool> UpdateStatusAsync(int requestId, string status)
        {
            if (!Enum.TryParse(status, out RequestStatus parsedStatus))
                return false;

            var updatedRequests = await _repository.UpdateStatusAsync(requestId, parsedStatus);
            return updatedRequests.Any();
        }
    }
}
