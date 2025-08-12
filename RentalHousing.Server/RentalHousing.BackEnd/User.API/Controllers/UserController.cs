using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Application.DTOs;
using User.Application.Interface;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;  

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationToken>> Register([FromBody] RegisterDto userRegistrationDto)
        {
            try
            {
               // return await RegisterUserAsync(userRegistrationDto);


                if (userRegistrationDto == null)
                {
                    return BadRequest("User registration data is required.");
                }
                var result = await _userService.RegisterAsync(userRegistrationDto);
                
                    return Ok(result);
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }

        }
        [HttpPost("login")]
        public async Task<ActionResult<ApplicationToken>> Login([FromBody] LoginDto userLoginDto)
        {
            try
            {
                if (userLoginDto == null)
                {
                    return BadRequest("User login data is required.");
                }
                var result = await _userService.AuthenticateAsync(userLoginDto);
                if (result != null)
                {
                    return Ok(result);
                }
                return Unauthorized("Invalid username or password.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApplicationToken>> RefreshToken([FromQuery] string refreshToken, [FromQuery] string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(userId))
                {
                    return BadRequest("Refresh token and user ID are required.");
                }
                var result = await _userService.GetTokenViaRefreshTokenAsync(refreshToken, userId);
                if (result != null)
                {
                    return Ok(result);
                }
                return Unauthorized("Invalid refresh token or user ID.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromQuery] string userId, [FromQuery] string refreshToken)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(refreshToken))
                {
                    return BadRequest("User ID and refresh token are required.");
                }
                var result = await _userService.LogoutAsync(userId, refreshToken);
                if (result)
                {
                    return Ok("Logout successful.");
                }
                return Unauthorized("Invalid user ID or refresh token.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("profile/{userId}")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return BadRequest("User ID is required.");
                }
                var result = await _userService.GetUserProfileAsync(userId);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("User profile not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

    }
}
