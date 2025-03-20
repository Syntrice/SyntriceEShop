using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;
using SyntriceEShop.API.Services;
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
        var result = await userService.GetAllProductsByUserAsync(userId);
        
        if (result.Type == ServiceResponseType.Success)
        {
            return Ok(result.Value);
        }
        
        if (result.Type == ServiceResponseType.NotFound)
        {
            return NotFound();
        }

        return StatusCode(500);
    }
    
    [HttpGet]
    [Route("{id:int}/orders")]
    public async Task<IActionResult> GetAllOrdersByUserAsync([FromRoute] int userId)
    {
        var result = await userService.GetAllOrdersByUserAsync(userId);
        
        if (result.Type == ServiceResponseType.Success)
        {
            return Ok(result.Value);
        }
        
        if (result.Type == ServiceResponseType.NotFound)
        {
            return NotFound();
        }

        return StatusCode(500);
    }
    
    [HttpGet]
    [Route("{id:int}/shopping-cart")]
    public async Task<IActionResult> GetShoppingCartByUserAsync([FromRoute] int userId)
    {
        var result = await userService.GetShoppingCartByUserAsync(userId);
        
        if (result.Type == ServiceResponseType.Success)
        {
            return Ok(result.Value);
        }
        
        if (result.Type == ServiceResponseType.NotFound)
        {
            return NotFound();
        }
        
        return StatusCode(500);
    }
}