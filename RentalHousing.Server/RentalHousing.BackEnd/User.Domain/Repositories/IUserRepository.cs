namespace User.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User.Domain.Entities.User> GetUserByUserEmailAsync(string email);
        Task<User.Domain.Entities.User> GetUserByUserNameAsync(string userName);

        Task<User.Domain.Entities.User> GetUserByIdAsync(Guid id);

        Task<User.Domain.Entities.User> CreateUserAsync(User.Domain.Entities.User user, string password);
        Task<bool> UpdateUserAsync(User.Domain.Entities.User user);
        Task<List<string>> GetUserRolesAsync(User.Domain.Entities.User user);

        Task<bool> AddUserRoleAsync(User.Domain.Entities.User user, string roleName);


        Task<bool> IsTwoFactoryEnable(Guid userId);
        Task<bool> EnableTwoFactory(Guid userId, bool enable);
        
        Task<bool> IsUserExistAsync(Guid userId);

        Task<bool> IsUserExistByEmailAsync(string email);

        Task<bool> IsValidUserIdPassword(User.Domain.Entities.User user, string password);

        Task<bool> IsLockOut(Guid userId);
        Task<DateTime?> GetLockoutEndDateAsync(User.Domain.Entities.User user);
    }
}
