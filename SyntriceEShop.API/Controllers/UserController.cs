using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.UserServices;

namespace SyntriceEShop.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase, IUserController
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
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequestDTO userLoginRequestDto)
    {
        var result = await userService.LoginAsync(userLoginRequestDto);
        
        switch (result.Type)
        {
            case ServiceResponseType.Success:
                return Ok(result.Value);
            case ServiceResponseType.NotFound:
                return NotFound(result.Message);
            case ServiceResponseType.InvalidCredentials:
                return Unauthorized(result.Message);
            default:
                return StatusCode(500); // Fallback response
        }
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
       var userId = int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out int parsed) ? parsed : 0;
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
}