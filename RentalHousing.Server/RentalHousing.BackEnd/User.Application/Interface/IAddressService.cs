using User.Application.DTOs;

namespace User.Application.Interface
{
    public interface  IAddressService
    {

        Task<AddressDto> GetAddressById(Guid addressId);
        Task<List<AddressDto>> GetAllAddresses();
        Task<AddressDto> UpdateAddress(AddressDto addressDto);
        Task DeleteAddressById(Guid addressId, Guid userId);

        Task<List<AddressDto>> GetAddressByUserId(Guid userId);

        Task CreateAddressAsync(AddressDto addressDto, Guid userId);

    }
}
