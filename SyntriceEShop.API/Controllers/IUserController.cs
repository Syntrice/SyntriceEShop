using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Services.Models;

namespace SyntriceEShop.API.Controllers;

public interface IUserController
{
    Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequestDTO userRegisterRequestDto);
    Task<IActionResult> LoginAsync([FromBody] UserLoginRequestDTO userLoginRequestDto, [FromQuery] bool useCookies = false);

    Task<IActionResult> RefreshAsync([FromBody] UserRefreshRequestDTO userRefreshRequestDto, [FromQuery] bool useCookies = false);
}