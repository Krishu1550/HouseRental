using User.Domain.Entities;

namespace User.Domain.Repositories
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAddressesByUserIdAsync(Guid userId);
        Task<bool> AddAddressToUserAsync(Guid userId, Address address);
        Task<bool> RemoveAddressFromUserAsync(Guid userId, Guid addressId);
        Task<Address> GetAddressByIdAsync(Guid addressId);
        Task<bool> UpdateAddressAsync(Address address);
        Task<bool> IsAddressExistAsync(Guid addressId);
        Task<List<Address>> GetAllAddressesAsync();
    }
}
