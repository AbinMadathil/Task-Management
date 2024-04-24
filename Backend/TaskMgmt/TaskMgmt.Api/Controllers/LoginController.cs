using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Api.DTO;
using TaskMgmt.DataAccess.Repositories;
using TaskMgmt.Services;
using TaskMgmt.Services.CustomExceptions;
using TaskMgmt.Services.Logger;

namespace TaskMgmt.Api.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILoggerManager _logger;

        public LoginController(IUserService userService, ILoggerManager logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [Route("api/login")]
        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                string token = await _userService.Authenticate(loginDto.Email, loginDto.Password);
                _logger.LogInfo($"User '{loginDto.Email}' successfully authenticated.");
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError($"Authentication failed for user '{loginDto.Email}': {ex.Message}");
                return Unauthorized(ex.Message);
            }

        }
        [Route("api/signup")]
        [HttpPost()]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO signUpDto)
        {
            try
            {
                if (signUpDto.ReferralCode == null)
                {
                    string token = await _userService.SignUp(signUpDto.Email, signUpDto.Password, signUpDto.Name, signUpDto.GroupName);
                    _logger.LogInfo($"User '{signUpDto.Email}' SignUp successful.");
                    return Ok(token);
                }
                else
                {
                    string token = await _userService.SignUpWithReferral(signUpDto.Email, signUpDto.Password, signUpDto.Name, signUpDto.ReferralCode, signUpDto.GroupName);
                    _logger.LogInfo($"User '{signUpDto.Email}' SignUp with referral successful.");
                    return Ok(token);
                }
            }
            catch (GroupAlreadyExistsException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
            catch (UserAlreadyExistsException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }
            catch (GroupNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
