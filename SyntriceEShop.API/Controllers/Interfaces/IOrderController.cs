using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Models.OrderModel.DTO;

namespace SyntriceEShop.API.Controllers.Interfaces;

public interface IOrderController
{
    Task<IActionResult> GetAllOrdersAsync();
    Task<IActionResult> GetOrderByIdAsync([FromRoute] int id);
    Task<IActionResult> AddOrderAsync([FromBody] AddOrderRequest addOrderRequest);
    Task<IActionResult> DeleteOrderAsync([FromRoute] int id);
}