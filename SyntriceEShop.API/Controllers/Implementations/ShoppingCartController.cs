using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;
using SyntriceEShop.API.Models.ShoppingCartModel.DTO;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/shopping-cart")]
public class ShoppingCartController(IShoppingCartService shoppingCartService) : ControllerBase, IShoppingCartController
{
    [HttpPost]
    public async Task<IActionResult> AddShoppingCartAsync([FromBody] AddShoppingCartRequest addShoppingCartRequest)
    {
        var result = await shoppingCartService.AddShoppingCartAsync(addShoppingCartRequest);

        if (result.Type == ServiceResponseType.Success)
        {
            return Created();
        }

        if (result.Type == ServiceResponseType.Conflict)
        {
            return Conflict(result.Message);
        }

        if (result.Type == ServiceResponseType.ValidationError)
        {
            return BadRequest(result.Message);
        }
        
        return StatusCode(500);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteShoppingCartByIdAsync([FromRoute] int id)
    {
        var result = await shoppingCartService.DeleteShoppingCartAsync(id);
        
        
        if (result.Type == ServiceResponseType.Success)
        {
            return NoContent();
        }

        if (result.Type == ServiceResponseType.NotFound)
        {
            return NotFound();
        }

        return StatusCode(500);
    }
    
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateShoppingCartByIdAsync([FromRoute] int id, [FromBody] UpdateShoppingCartRequest updateShoppingCartRequest)
    {
        var result = await shoppingCartService.UpdateShoppingCartByIdAsync(id, updateShoppingCartRequest);
        
        if (result.Type == ServiceResponseType.Success)
        {
            return NoContent();
        }

        if (result.Type == ServiceResponseType.ValidationError)
        {
            return BadRequest(result.Message);
        }
        
        if (result.Type == ServiceResponseType.NotFound)
        {
            return NotFound();
        }
        
        return StatusCode(500);
    }
}