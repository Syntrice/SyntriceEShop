using Microsoft.AspNetCore.Mvc;

namespace SyntriceEShop.API.Controllers.Interfaces;

public interface IUserController
{
    Task<IActionResult> GetAllProductsByUserAsync();
    Task<IActionResult> GetAllOrdersByUserAsync();
    Task<IActionResult> GetShoppingCartByUserAsync();
}