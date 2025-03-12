using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Controllers;

public interface IUserController
{
    Task<OkObjectResult> RegisterAsync([FromBody] UserRegisterDTO userRegisterDTO);
    Task<OkResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO);
}