using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Database;
using SyntriceEShop.API.Utilities;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IPasswordHasher passwordHasher, ApplicationDbContext applicationDbContext) : ControllerBase, IUserController
{
    [HttpPost]
    [Route("register")]
    public async Task<OkObjectResult> RegisterAsync([FromBody] UserRegisterDTO userRegisterDTO)
    {
        // For now, business logic and data access is handled in the controller. Eventually,
        // this should be moved to a service and repository. and unit tested accordingly.
        
        var user = new User()
        {
            Username = userRegisterDTO.Username,
            PasswordHash = passwordHasher.Hash(userRegisterDTO.Password) // hash the password
        };
        
        var created = applicationDbContext.Users.Add(user);
        await applicationDbContext.SaveChangesAsync();
        
        return Ok(created.Entity);
    }

    [HttpPost]
    [Route("login")]
    public Task<OkResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO)
    {
        return Task.FromResult(Ok());
    }
}