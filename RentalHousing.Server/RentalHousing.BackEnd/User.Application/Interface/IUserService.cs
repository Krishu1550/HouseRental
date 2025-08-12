
using User.Application.DTOs;

namespace User.Application.Interface
{
    public interface IUserService
    {

        Task<ApplicationToken> AuthenticateAsync(LoginDto loginDto);
        Task<ApplicationToken> GetTokenViaRefreshTokenAsync(string refreshToken, string userId);
        Task<ApplicationToken> RegisterAsync(RegisterDto registerDto);

        Task<bool> LogoutAsync(string userId, string refreshToken);

        Task<UserProfileDto> GetUserProfileAsync(Guid userId);
    }
}
