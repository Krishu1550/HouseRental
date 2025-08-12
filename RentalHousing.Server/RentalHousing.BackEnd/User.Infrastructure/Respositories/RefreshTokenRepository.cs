
using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using User.Domain.Repositories;
using User.Infrastructure.Persistence;

namespace User.Infrastructure.Respositories
{
    public class RefreshTokenRepository: IRefreshTokenRepository
    {

        private readonly UserDBContext _context;
        public RefreshTokenRepository(UserDBContext userDBContext) 
        { 
         _context = userDBContext ?? throw new ArgumentNullException(nameof(userDBContext), "UserDBContext cannot be null");    

        }

        public async Task<string> CreateAsync(Guid userId, DateTime expiresAt)
        {

            RefreshToken refreshToken = new RefreshToken
            {
                UserId = userId,
                ExpiresAt = expiresAt,
                Token = Guid.NewGuid().ToString() // Generate a new token
            };
           await  _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            return refreshToken.Token;
        }

        public async Task<DateTime?> GetExpirationByTokenAsync(string token)
        { 
        
            var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken == null)
            {
                return null; // Token not found
            }   
            return refreshToken?.ExpiresAt;
        }

        public async Task<Guid?> GetUserIdByTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                  .FirstOrDefaultAsync(rt => rt.Token == token);
            return refreshToken?.UserId; // Return the UserId or null if not found
        }

        public Task<bool> IsValidAsync(string token, Guid userId)
        {
            return _context.RefreshTokens
                .AnyAsync(rt => rt.Token == token && rt.UserId == userId && rt.IsActive && !rt.IsRevoked);
        }

        public async Task<bool> RevokeAllAsync(Guid userId)
        {
          return await _context.RefreshTokens
                .Where(rt => rt.UserId == userId && rt.IsActive)
                .ExecuteUpdateAsync(rt => rt.SetProperty(r => r.RevokedAt, DateTime.UtcNow)) > 0;
        }

        public async Task<bool> RevokeAsync(string token, Guid userId)
        {
             return await _context.RefreshTokens
                .Where(rt => rt.Token == token && rt.UserId == userId && rt.IsActive)
                .ExecuteUpdateAsync(rt => rt.SetProperty(r => r.RevokedAt, DateTime.UtcNow)) > 0;
        }


        // Implement the methods defined in IRefreshTokenRepository here
    }
}
