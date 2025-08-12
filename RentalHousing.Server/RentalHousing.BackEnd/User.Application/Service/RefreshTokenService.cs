using User.Application.Interface;
using User.Domain.Repositories;

namespace User.Application.Service
{
    public class RefreshTokenService: IRefreshTokenService
    {
        public IRefreshTokenRepository _tokenService;
        public RefreshTokenService(IRefreshTokenRepository refreshToken) 
        { 
            _tokenService = refreshToken;
        
        }

        public Task<bool> IsValidRefreshTokenAsync(string refreshToken, string userId)
        {
            return _tokenService.IsValidAsync(refreshToken, new Guid(userId));
                
        }

        public Task<bool> InValidAllRefreshTokenAsync(string refreshToken, string userId)
        {
            return _tokenService.RevokeAllAsync(new Guid(userId));    
        }

       
    }
}
