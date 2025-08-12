namespace User.Application.Interface
{
    public interface IRefreshTokenService
    {
        Task<bool> IsValidRefreshTokenAsync(string refreshToken, string userId);    
        Task<bool> InValidAllRefreshTokenAsync(string refreshToken, string userId);

    }
}
