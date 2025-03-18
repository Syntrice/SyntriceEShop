using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Models.AuthModel.DTO;

namespace SyntriceEShop.API.Controllers.Interfaces;

public interface IAuthController
{
    Task<IActionResult> RegisterAsync([FromBody] AuthRegisterRequest authRegisterRequest);
    Task<IActionResult> LoginAsync([FromBody] AuthLoginRequest authLoginRequest, [FromQuery] bool useCookies = false);

    Task<IActionResult> RefreshAsync([FromBody] AuthRefreshRequest authRefreshRequest, [FromQuery] bool useCookies = false);
}