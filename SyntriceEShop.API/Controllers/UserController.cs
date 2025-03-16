using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SyntriceEShop.API.ApplicationOptions;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.UserServices;

namespace SyntriceEShop.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService, IOptions<JWTOptions> jwtOptions) : ControllerBase, IUserController
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequestDTO userRegisterRequestDto)
    {
        var result = await userService.RegisterAsync(userRegisterRequestDto);

        switch (result.Type)
        {
            case ServiceResponseType.Success:
                return Ok();
            case ServiceResponseType.Conflict:
                return Conflict(result.Message);
            default:
                return StatusCode(500); // Fallback response
        }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequestDTO userLoginRequestDto,
        [FromQuery] bool useCookies = false)
    {
        var result = await userService.LoginAsync(userLoginRequestDto);

        if (result is { Type: ServiceResponseType.Success, Value: not null })
        {
            //  Whether to send the tokens as cookies or not
            if (useCookies)
            {
                // Set cookies for both access token and refresh token
                Response.Cookies.Append("accessToken", result.Value.AccessToken, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddMinutes(jwtOptions.Value.ExpirationInMinutes),
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    // unfortunately we have to use SameSiteMode.None as this is an API and will be called by a separate
                    // front end server likely on a different domain. 
                    SameSite = SameSiteMode.None 
                });
                
                Response.Cookies.Append("refreshToken", result.Value.RefreshToken, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpirationInDays),
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    // unfortunately we have to use SameSiteMode.None as this is an API and will be called by a separate
                    // front end server likely on a different domain. 
                    SameSite = SameSiteMode.None 
                });
                return Ok();
            }
            else
            {
                // Send the tokens in the response rather than as cookies
                return Ok(result.Value);
            }

            return Ok(result.Value);
        }

        if (result.Type == ServiceResponseType.NotFound)
        {
            return NotFound(result.Message);
        }

        if (result.Type == ServiceResponseType.InvalidCredentials)
        {
            return Unauthorized(result.Message);
        }

        return StatusCode(500);
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> RefreshAsync([FromBody] UserRefreshRequestDTO userRefreshRequestDto)
    {
        var result = await userService.RefreshAsync(userRefreshRequestDto);

        switch (result.Type)
        {
            case ServiceResponseType.Success:
                return Ok(result.Value);
            case ServiceResponseType.InvalidCredentials:
                return Unauthorized(result.Message);
            default:
                return StatusCode(500); // Fallback response
        }
    }

    // TODO: Unit test
    [HttpDelete]
    [Route("{id:int}/refresh-tokens")]
    public async Task<IActionResult> RevokeRefreshTokensAsync([FromRoute] int id)
    {
        // check if user matches logged-in user
        var userId = int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int parsed)
            ? parsed
            : 0;
        if (id != userId)
        {
            return Unauthorized();
        }

        var result = await userService.RevokeRefreshTokensAsync(id);

        switch (result.Type)
        {
            case ServiceResponseType.Success:
                return Ok();
            default:
                return StatusCode(500); // Fallback response
        }
    }

    // Test authentication
    [HttpGet]
    [Authorize]
    [Route("test-authentication")]
    public async Task<IActionResult> TestAuthenticationAsync()
    {
        var username = HttpContext.User.FindFirstValue("username");
        return Ok($"Hi there, {username}. You are authenticated!");
    }
}