using Microsoft.AspNetCore.Mvc;

namespace SyntriceEShop.API.Controllers.Interfaces;

public interface IUserController
{
    Task<IActionResult> GetAllProductsByUserAsync(int userId);
    Task<IActionResult> GetAllOrdersByUserAsync(int userId);
    Task<IActionResult> GetShoppingCartByUserAsync(int userId);
}