using Appartment.Application.DTOs;
using Appartment.Application.Interface;
using Appartment.Domain.Entities;
using Appartment.Domain.Repositories;
using AutoMapper;


namespace Appartment.Application.Services
{
        public class ApartmentService : IApartmentService
        {
            private readonly IAppartmentRepository _repository;
            private readonly IMapper _mapper;

            public ApartmentService(IAppartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

            public async Task<ApartmentDto> AddApartmentAsync(CreateApartmentDto dto)
        {
            var apartment = _mapper.Map<Apartment>(dto);
            var created = await _repository.AddAsync(apartment);
            return _mapper.Map<ApartmentDto>(created);
        }

            public async Task DeleteApartmentAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

            public async Task<IEnumerable<ApartmentDto>> GetAllAsync()
        {
            var apartments = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ApartmentDto>>(apartments);
        }

            public async Task<ApartmentDto?> GetByIdAsync(int id)
        {
            var apartment = await _repository.GetByIdAsync(id);
            return apartment == null ? null : _mapper.Map<ApartmentDto>(apartment);
        }

            public async Task<IEnumerable<ApartmentDto>> GetByOwnerIdAsync(string ownerId)
        {
            var apartments = await _repository.GetByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<ApartmentDto>>(apartments);
        }

            public async Task<IEnumerable<ApartmentDto>> SearchAsync(string city, decimal? minPrice, decimal? maxPrice, int? bedrooms)
        {
            var apartments = await _repository.SearchAsync(city, minPrice, maxPrice, bedrooms);
            return _mapper.Map<IEnumerable<ApartmentDto>>(apartments);
        }

            public async Task UpdateApartmentAsync(UpdateApartmentDto dto)
        {
            var apartment = _mapper.Map<Apartment>(dto);
            await _repository.UpdateAsync(apartment);
        }
        }
    }
