using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;
using SyntriceEShop.API.Models.OrderModel.DTO;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase, IOrderController
{
    [HttpGet]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetOrderByIdAsync([FromRoute] int id)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddOrderAsync([FromBody] AddOrderRequest addOrderRequest)
    {
        return Ok();
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteOrderByIdAsync([FromRoute] int id)
    {
        return Ok();
    }
}