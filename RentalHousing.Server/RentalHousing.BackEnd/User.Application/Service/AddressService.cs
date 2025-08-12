using AutoMapper;
using User.Application.DTOs;
using User.Application.Interface;
using User.Domain.Repositories;

namespace User.Application.Service
{
    public class AddressService : IAddressService
    {
        private IAddressRepository _addressRepository;

        IMapper _mapper;
        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {

            _addressRepository = addressRepository;
            _mapper = mapper;

        }

        public async Task DeleteAddressById(Guid addressId, Guid userId)
        {
            await _addressRepository.RemoveAddressFromUserAsync(userId, addressId);
        }

        public async Task<AddressDto> GetAddressById(Guid addressId)
        {
            return _mapper.Map<AddressDto>(await _addressRepository.GetAddressByIdAsync(addressId));
        }

        public Task<List<AddressDto>> GetAllAddresses()
        {
            return Task.FromResult(_mapper.Map<List<AddressDto>>(_addressRepository.GetAllAddressesAsync()));
        }

        public async Task<AddressDto> UpdateAddress(AddressDto addressDto)
        {
            return _mapper.Map<AddressDto>(await _addressRepository.UpdateAddressAsync(_mapper.Map<Domain.Entities.Address>(addressDto)));
        }

        public async Task CreateAddressAsync(AddressDto addressDto, Guid userId)
        {
            var address = _mapper.Map<Domain.Entities.Address>(addressDto);
            // Assuming UserId is a property in Address entity
            await _addressRepository.AddAddressToUserAsync(userId, address);

        }

        public async Task<List<AddressDto>> GetAddressByUserId(Guid userId)
        {
            var addresses = await _addressRepository.GetAddressesByUserIdAsync(userId);
            return _mapper.Map<List<AddressDto>>(addresses);
        }
    }
}
