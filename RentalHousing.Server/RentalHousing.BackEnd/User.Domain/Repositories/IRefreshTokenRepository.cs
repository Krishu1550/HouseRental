using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {

        Task<bool> IsValidAsync(string token, Guid userId);
        Task<bool> RevokeAsync(string token, Guid userId);
        Task<bool> RevokeAllAsync(Guid userId);
        Task<string> CreateAsync(Guid userId, DateTime expiresAt);
        Task<Guid?> GetUserIdByTokenAsync(string token);
        Task<DateTime?> GetExpirationByTokenAsync(string token);
    }
}
