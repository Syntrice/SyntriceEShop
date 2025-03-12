using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Services;
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
        
        return Ok(result.Value);
    }

    [HttpPost]
    [Route("login")]
    public Task<IActionResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}