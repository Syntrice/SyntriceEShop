using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Models.ShoppingCartModel.DTO;

namespace SyntriceEShop.API.Controllers.Interfaces;

public interface IShoppingCartController
{
    Task<IActionResult> AddShoppingCartAsync([FromBody] AddShoppingCartRequest addShoppingCartRequest);
    Task<IActionResult> DeleteShoppingCartByIdAsync([FromRoute] int id);
    Task<IActionResult> UpdateShoppingCartByIdAsync([FromRoute] int id, [FromBody] UpdateShoppingCartRequest updateShoppingCartRequest);
}