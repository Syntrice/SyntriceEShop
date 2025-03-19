using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;
using SyntriceEShop.API.Models.OrderModel.DTO;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/order")]
public class OrderController(IOrderService orderService) : ControllerBase, IOrderController
{
    [HttpGet]
    public async Task<IActionResult> GetAllOrdersAsync()
    {
        var result = await orderService.GetAllOrdersAsync();
        
        if (result.Type == ServiceResponseType.Success)
        {
            return Ok(result.Value);
        }
        
        return StatusCode(500);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetOrderByIdAsync([FromRoute] int id)
    {   
        var result = await orderService.GetOrderByIdAsync(id);

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
    
    [HttpPost]
    public async Task<IActionResult> AddOrderAsync([FromBody] AddOrderRequest addOrderRequest)
    {
        var result = await orderService.AddOrderAsync(addOrderRequest);
        
        if (result.Type == ServiceResponseType.Success)
        {
            return Ok(result.Value);
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
    public async Task<IActionResult> DeleteOrderByIdAsync([FromRoute] int id)
    {
        var result = await orderService.DeleteOrderByIdAsync(id);
        
        if (result.Type == ServiceResponseType.Success)
        {
            return Ok();
        }
        
        if (result.Type == ServiceResponseType.NotFound)
        {
            return NotFound();
        }
        
        return StatusCode(500);
    }
}