using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using User.Domain.Repositories;
using User.Infrastructure.Persistence;

namespace User.Infrastructure.Respositories
{
    public class AddressRepository: IAddressRepository
    {
        private UserDBContext _context; 

        public AddressRepository( UserDBContext userDBContext )
        {
            // Initialize any resources or dependencies here if needed
            _context= userDBContext ?? throw new ArgumentNullException(nameof(userDBContext), "UserDBContext cannot be null");
        }

        public async Task<bool> AddAddressToUserAsync(Guid userId, Address address)
        {
            Address existingAddress= await  _context.Addresses.FirstOrDefaultAsync(a => a.Id == address.Id);
            if (existingAddress != null)
            {
                throw new InvalidOperationException("Address already exists in the database.");
            }
           
            existingAddress.UserId = userId;
           // existingAddress.CreatedAt = DateTime.UtcNow;
            existingAddress.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Address> GetAddressByIdAsync(Guid addressId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
        }

        public async Task<List<Address>> GetAddressesByUserIdAsync(Guid userId)
        {
           return await _context.Addresses
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Address>> GetAllAddressesAsync()
        {
            return await _context.Addresses.ToListAsync();  
        }

        public async Task<bool> IsAddressExistAsync(Guid addressId)
        {
            return await _context.Addresses.AnyAsync(a => a.Id == addressId);
        }

        public async Task<bool> RemoveAddressFromUserAsync(Guid userId, Guid addressId)
        {
            Address address = _context.Addresses.FirstOrDefault(a => a.Id == addressId && a.UserId == userId);
            if (address == null)
            {
                throw new InvalidOperationException("Address not found for the specified user.");
            }
            _context.Addresses.Remove(address);
           await  _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAddressAsync(Address address)
        {
                      
            Address existingAddress = _context.Addresses.FirstOrDefault(a => a.Id == address.Id);
            if (existingAddress == null)
            {
                throw new InvalidOperationException("Address not found in the database.");
            }
            existingAddress.Street = address.Street;
            existingAddress.City = address.City;
            existingAddress.State = address.State;
            existingAddress.ZipCode = address.ZipCode;
            existingAddress.Country = address.Country;
            existingAddress.UpdatedAt = DateTime.UtcNow;
            await  _context.SaveChangesAsync();

            return true;

        }
    }
}
