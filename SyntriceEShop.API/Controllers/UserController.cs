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

    [HttpGet]
    [Authorize]
    [Route("testAuth")]
    public IActionResult TestAuth()
    {
        return Ok("You are authenticated!");
    }
}