using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.UserServices;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase, IUserController
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDTO userRegisterDTO)
    {
        var result = await userService.RegisterAsync(userRegisterDTO);

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
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
    {
        var result = await userService.LoginAsync(userLoginDTO);
        
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