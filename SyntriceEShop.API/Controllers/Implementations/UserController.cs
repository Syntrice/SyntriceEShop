using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase, IUserController
{
    [HttpGet]
    [Route("{id:int}/products")]
    public async Task<IActionResult> GetAllProductsByUserAsync([FromRoute] int userId)
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{id:int}/orders")]
    public async Task<IActionResult> GetAllOrdersByUserAsync([FromRoute] int userId)
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{id:int}/shopping-cart")]
    public async Task<IActionResult> GetShoppingCartByUserAsync([FromRoute] int userId)
    {
        return Ok();
    }
    

}