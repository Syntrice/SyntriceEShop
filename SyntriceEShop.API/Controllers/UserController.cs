using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public Task<OkResult> RegisterAsync([FromBody] UserRegisterDTO userRegisterDTO)
    {
        return Task.FromResult(Ok());
    }

    [HttpPost]
    [Route("login")]
    public Task<OkResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
    {
        return Task.FromResult(Ok());
    }
}