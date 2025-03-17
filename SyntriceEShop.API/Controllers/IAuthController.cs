using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Models.UserModel.DTO;

namespace SyntriceEShop.API.Controllers;

public interface IAuthController
{
    Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequest userRegisterRequest);
    Task<IActionResult> LoginAsync([FromBody] UserLoginRequest userLoginRequest, [FromQuery] bool useCookies = false);

    Task<IActionResult> RefreshAsync([FromBody] UserRefreshRequest userRefreshRequest, [FromQuery] bool useCookies = false);
}