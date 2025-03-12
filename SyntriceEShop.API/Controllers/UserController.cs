using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Response;
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
                return Ok(result.Value);
            case ServiceResponseType.Conflict:
                return Conflict(result.Message);
            default:
                return StatusCode(500); // Fallback response
        }
    }

    [HttpPost]
    [Route("login")]
    public Task<IActionResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}