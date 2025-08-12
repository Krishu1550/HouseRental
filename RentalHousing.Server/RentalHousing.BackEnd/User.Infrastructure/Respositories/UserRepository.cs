using Microsoft.AspNetCore.Identity;
using User.Domain.Repositories;
using User.Infrastructure.Identity;
using User.Infrastructure.Persistence;

namespace User.Infrastructure.Respositories
{
    public class UserRepository : IUserRepository
    {
        private UserManager<ApplicationUser> _userManager;  
        private UserDBContext _context;
        public UserRepository(UserDBContext userDBContext, UserManager<ApplicationUser> userManager)
        {
            _context = userDBContext ?? throw new ArgumentNullException(nameof(userDBContext)); 
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager)); 
        }
        private ApplicationUser DomainToApplicationUser(Domain.Entities.User user)
        {
            if (user == null) return null;
            return new ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }   

        private Domain.Entities.User ApplicationUserToDomain(ApplicationUser appUser)
        {
            if (appUser == null) return null;
            return new Domain.Entities.User
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                PhoneNumber = appUser.PhoneNumber,
                Name = appUser.Name,
                CreatedAt = appUser.CreatedAt,
                UpdatedAt = appUser.UpdatedAt
            };
        }
        

        public async Task<bool> AddUserRoleAsync(Domain.Entities.User user, string roleName)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(user.Id.ToString());

            await  _userManager.AddToRoleAsync(applicationUser, roleName);
            if(applicationUser== null)
            {
                return false;
            }
            return true;
        }

        public async Task<Domain.Entities.User> CreateUserAsync(Domain.Entities.User user, string password)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                Id = new Guid(),
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _userManager.CreateAsync(applicationUser, password);
            return ApplicationUserToDomain(await _userManager.FindByEmailAsync(user.Email));

        }

        public async Task<bool> EnableTwoFactory(Guid userId, bool enable)
        {
          await _userManager.SetTwoFactorEnabledAsync(await _userManager.FindByIdAsync(userId.ToString()), enable);

          return true;
        }

        public async Task<DateTime?> GetLockoutEndDateAsync(Domain.Entities.User user)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(user.Id.ToString());
            if (applicationUser == null)
            {
                return null;
            }
           var date  = await _userManager.GetLockoutEndDateAsync(applicationUser);


            return date.Value.Date;
        }

        public async Task<Domain.Entities.User> GetUserByIdAsync(Guid id)
        {
           return ApplicationUserToDomain(await _userManager.FindByIdAsync(id.ToString()));
        }

        public async Task<Domain.Entities.User> GetUserByUserEmailAsync(string email)
        {
            return  ApplicationUserToDomain(await _userManager.FindByEmailAsync(email));    
        }

        public async Task<Domain.Entities.User> GetUserByUserNameAsync(string userName)
        {
            return  ApplicationUserToDomain(await _userManager.FindByNameAsync(userName));
        }

        public async Task<List<string>> GetUserRolesAsync(Domain.Entities.User user)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(user.Id.ToString());
            if (applicationUser == null)
            {
                return new List<string>();
            }
            var roles= await _userManager.GetRolesAsync(applicationUser);
            return roles.ToList();
            
        }

        public async Task<bool> IsLockOut(Guid userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return false;

            }

           return await  _userManager.IsLockedOutAsync(user);

        }

        public async Task<bool> IsTwoFactoryEnable(Guid userId)
        {
           ApplicationUser applicationUser= await  _userManager.FindByIdAsync(userId.ToString());
          return  await  _userManager.GetTwoFactorEnabledAsync(applicationUser);

        }

        public async Task<bool> IsUserExistAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString()) != null; 
        }

        public async Task<bool> IsUserExistByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        
        public async Task<bool> UpdateUserAsync(Domain.Entities.User user)
        {
            ApplicationUser applicationUser=  await _userManager.FindByIdAsync(user.Id.ToString());
            if (applicationUser == null)
            {
                return false;
            }

            applicationUser.UserName = user.UserName;
            applicationUser.Email = user.Email;
            applicationUser.PhoneNumber = user.PhoneNumber;
            applicationUser.Name = user.Name;
            applicationUser.UpdatedAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(applicationUser);

            return true;    

        }
        public async  Task<bool> IsValidUserIdPassword(User.Domain.Entities.User user, string password)
        {
           ApplicationUser applicationUser=await   _userManager.FindByEmailAsync(user.Email);

            if (applicationUser == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(applicationUser, password); 
        }

    }
}
