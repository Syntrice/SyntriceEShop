using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Controllers;

public interface IUserController
{
    Task<IActionResult> RegisterAsync([FromBody] UserRegisterDTO userRegisterDTO);
    Task<IActionResult> LoginAsync([FromBody] UserLoginDTO userLoginDTO);
}