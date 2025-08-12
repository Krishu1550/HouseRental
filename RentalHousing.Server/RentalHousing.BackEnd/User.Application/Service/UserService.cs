using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Application.DTOs;
using User.Application.Interface;
using User.Domain.Repositories;


namespace User.Application.Service
{
    public  class UserService: IUserService
    {

        private  IUserRepository _user;
        private IRefreshTokenRepository _refresh;
        private IConfiguration _configuration;
        public UserService(IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IConfiguration configuration) 
        { 
            _user = userRepository;
            _refresh= refreshTokenRepository;
            _configuration = configuration;

        }
        public async Task<ApplicationToken> AuthenticateAsync(LoginDto loginDto)
        {
            User.Domain.Entities.User user;
            if (loginDto == null)
            {
                throw new ArgumentNullException(
                    nameof(loginDto));
            }

            if (loginDto.UserNameOrEmail.Contains("@"))
            {

                user = await _user.GetUserByUserEmailAsync(loginDto.UserNameOrEmail);
            }
            else
            {  

                user= await _user.GetUserByUserNameAsync(loginDto.UserNameOrEmail); 
            }

            bool isValidUser= await _user.IsValidUserIdPassword(user, loginDto.Password);


            if (!isValidUser)
            {

                return null;
            }

            List<string> roles = await _user.GetUserRolesAsync(user);


            return new ApplicationToken
            {
                RefreshToken= await _refresh.CreateAsync(user.Id,DateTime.Now.AddDays(30)),   
                Token = GenerateJWTToken(user, roles)

            };

            
        }

        public async Task<ApplicationToken> GetTokenViaRefreshTokenAsync(string refreshToken, string userId)
        {
            var user = await _user.GetUserByIdAsync(new Guid(userId));
            if (user == null)
            {
                throw new InvalidDataException("Invalid User Id");
            }
            var isValid = await _refresh.IsValidAsync(refreshToken , new Guid( userId));
            if (!isValid)
            {
                throw new InvalidDataException("Invalid Refresh Token");
            }
            List<string> roles = await _user.GetUserRolesAsync(user);
            return new ApplicationToken
            {
                RefreshToken = await _refresh.CreateAsync(new Guid(userId), DateTime.Now.AddDays(30)),
                Token = GenerateJWTToken(user, roles)
            };

        }

        public Task<bool> LogoutAsync(string userId, string refreshToken)
        {
            
          return   _refresh.RevokeAllAsync(new Guid(userId));
        }

        public async Task<ApplicationToken> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                throw new ArgumentNullException(nameof(registerDto));
            }

            var user = _user.GetUserByUserEmailAsync(registerDto.UserName);

           // var user = _user.GetUserByUserEmailAsync(registerDto.UserName);
            if (user!=null)
            {
                throw new InvalidDataException("Invaild User Emails is already exist");
            }

            User.Domain.Entities.User newUser = new User.Domain.Entities.User()
            {
                Id= new Guid(),
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Name = registerDto.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt= DateTime.Now,
                PhoneNumber=registerDto.PhoneNumber,
                Roles = registerDto.Roles,
            };
            if(registerDto.Password== registerDto.ConfirmPassword)
            {
                User.Domain.Entities.User existingUser= 
                    await _user.CreateUserAsync(newUser, registerDto.ConfirmPassword);

                if (existingUser!=null) 
                {
                        return null;
                }
              
            LoginDto loginDto = new LoginDto()
                     {
                        UserNameOrEmail = registerDto.Email,
                        Password = registerDto.Password
                     };

                return  await   AuthenticateAsync(loginDto);
                
                
            }
            return null;
            
        }

        private string GenerateJWTToken(User.Domain.Entities.User user, List<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email ),
                new Claim(ClaimTypes.Name, user.Name ),
            };
            foreach (var role in roles)
            {

                claims.Add( new Claim(ClaimTypes.Role, role));
            }
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var expiryMinutes = Convert.ToInt32(_configuration["JwtSettings:TokenExpiryMinutes"]);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds= new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                           issuer: issuer,
                         // optional if you’re not validating audience
                           claims: claims,
                           expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                           signingCredentials: creds
                       );


            // public JwtSecurityToken(string issuer = null, string audience = null,
            // IEnumerable<Claim> claims = null, DateTime? notBefore = null, DateTime?
            // expires = null, SigningCredentials signingCredentials = null)
           

                return new JwtSecurityTokenHandler().WriteToken(token);
        
        }

        public async Task<UserProfileDto> GetUserProfileAsync(Guid userId)
        {

            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId), "User ID cannot be empty.");
            }
            var user = await _user.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidDataException("User not found.");
            }
            return new UserProfileDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

        }

    }
}
