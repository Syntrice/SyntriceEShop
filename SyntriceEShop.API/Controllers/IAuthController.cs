using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Models.AuthModel.DTO;
using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Controllers;

public interface IAuthController
{
    Task<IActionResult> RegisterAsync([FromBody] AuthRegisterRequest authRegisterRequest);
    Task<IActionResult> LoginAsync([FromBody] AuthLoginRequest authLoginRequest, [FromQuery] bool useCookies = false);

    Task<IActionResult> RefreshAsync([FromBody] AuthRefreshRequest authRefreshRequest, [FromQuery] bool useCookies = false);
}