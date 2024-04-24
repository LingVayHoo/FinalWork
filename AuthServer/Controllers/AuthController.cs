using AuthServer.Models;
using AuthServer.Models.Auth;
using AuthServer.Models.Requests;
using AuthServer.Models.Responce;
using AuthServer.Services.Authenticators;
using AuthServer.Services.RefreshTokenRepositories;
using AuthServer.Services.TokenGenerators;
using AuthServer.Services.TokenValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthServer.Controllers
{
    // h ttps://localhost:7195/
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IdentityErrorDescriber _identityErrorDescriber;
        private readonly Authenticator _authenticator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        private static readonly string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public AuthController(UserManager<User> userManager,
            IdentityErrorDescriber identityErrorDescriber,
            Authenticator authenticator,
            RefreshTokenValidator refreshTokenValidator,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _identityErrorDescriber = identityErrorDescriber;
            _authenticator = authenticator;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpPost]
        [Route("FWregister")]
        public async Task<IActionResult> Register([FromBody] FWRegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errorMessages = ModelState.Values.SelectMany(
                    v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errorMessages);
            }
            if (registerRequest.Password != registerRequest.ConfirmPassword) return BadRequest();

            User registrationUser = new();

            registrationUser.UserName = registerRequest.Username;
            if (registerRequest.Role != null) registrationUser.Role = registerRequest.Role;
            else if (registerRequest.Username == "Admin") registrationUser.Role = "Admin";
            else registrationUser.Role = "Guest";

            IdentityResult result = await _userManager.CreateAsync(
                registrationUser, registerRequest.Password);
            if (!result.Succeeded)
            {
                IdentityError? primaryError = result.Errors.FirstOrDefault();

                if (primaryError != null)
                {
                    if (primaryError.Code == nameof(_identityErrorDescriber.DuplicateUserName))
                    {
                        return Conflict(new ErrorResponce("Username already exists"));
                    }
                }
            }

            return Ok();
        }

        [HttpPost]
        [Route("FWlogin")]
        public async Task<IActionResult> Login(
            [FromBody] FWLoginRequest loginRequest)
        {

            string t = string.Empty;
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errorMessages = ModelState.Values.SelectMany(
                    v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errorMessages);
            }

            User? user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                return Unauthorized();
            }

            bool isCorrectPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isCorrectPassword)
            {
                return Unauthorized();
            }

            

            AuthenticatedUserResponce responce = await _authenticator.Authenticate(user);
            Response.Cookies.Append("tasty-cookies", responce.AccessToken);
            //HttpContext.Response.Cookies["tasty-cookies"].Value = responce.AccessToken;
            return Ok(responce);
        }

        [HttpPost]
        [Route("FWrefresh")]
        public async Task<IActionResult> Refresh([FromBody] FWRefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errorMessages = ModelState.Values.SelectMany(
                    v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(errorMessages);
            }

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if(!isValidRefreshToken)
            {
                return BadRequest(new ErrorResponce("Invalid refresh token."));
            }

            RefreshToken refreshTokenDTO = 
                await _refreshTokenRepository.GetByToken(refreshRequest.RefreshToken);
            if(refreshTokenDTO == null)
            {
                return NotFound(new ErrorResponce("Invalid refresh token"));
            }

            await _refreshTokenRepository.Delete(refreshTokenDTO.Id);

            User? user = await _userManager.FindByIdAsync(refreshTokenDTO.UserId.ToString());
            if(user == null)
            {
                return NotFound(new ErrorResponce("User not found."));
            }

            AuthenticatedUserResponce responce = await _authenticator.Authenticate(user);
            return Ok(responce);

        }

        [Authorize]
        [HttpPost]
        [Route("FWlogout")]
        public async Task<IActionResult> Logout([FromBody] string? rawUserId)
        {
            //string? rawUserId = HttpContext.User.FindFirstValue("id");
            if (!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            await _refreshTokenRepository.DeleteAll(userId);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("test")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
