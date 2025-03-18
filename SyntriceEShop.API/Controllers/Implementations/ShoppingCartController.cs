using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;
using SyntriceEShop.API.Models.ShoppingCartModel.DTO;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/shopping-cart")]
public class ShoppingCartController : ControllerBase, IShoppingCartController
{
    [HttpPost]
    public async Task<IActionResult> AddShoppingCartAsync([FromBody] AddShoppingCartRequest addShoppingCartRequest)
    {
        return Ok();
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteShoppingCartByIdAsync([FromRoute] int id)
    {
        return Ok();
    }
    
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateShoppingCartByIdAsync([FromRoute] int id, [FromBody] UpdateShoppingCartRequest updateShoppingCartRequest)
    {
        return Ok();
    }
}