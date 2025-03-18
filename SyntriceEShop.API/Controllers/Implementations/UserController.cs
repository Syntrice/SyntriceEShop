using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase, IUserController
{
    [HttpGet]
    [Route("products")]
    public async Task<IActionResult> GetAllProductsByUserAsync()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("orders")]
    public async Task<IActionResult> GetAllOrdersByUserAsync()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("shopping-cart")]
    public async Task<IActionResult> GetShoppingCartByUserAsync()
    {
        return Ok();
    }
    

}