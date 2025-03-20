using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;
using SyntriceEShop.API.Models.ProductModel.DTO;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/product")]
public class ProductController(IProductService productService) : ControllerBase, IProductController
{
    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        var result = await productService.GetAllProductsAsync();
        
        if (result.Type == ServiceResponseType.Success)
        {
            return Ok(result.Value);
        }

        return StatusCode(500);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetProductByIdAsync([FromRoute] int id)
    {
        var result = await productService.GetProductByIdAsync(id);

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
    public async Task<IActionResult> AddProductAsync([FromBody] AddProductRequest addProductRequest)
    {
        var result = await productService.AddProductAsync(addProductRequest);
        
        if (result.Type == ServiceResponseType.Success)
        {
            return CreatedAtRoute("GetProductByIdAsync", new { id = result.Value });
        }
        
        if (result.Type == ServiceResponseType.Conflict)
        {
            return Conflict(result.Message);
        }
        
        if (result.Type == ServiceResponseType.ValidationError)
        {
            return BadRequest(result.Message);
        }
        
        return Ok();
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteProductByIdAsync([FromRoute] int id)
    {
        var result = await productService.DeleteProductByIdAsync(id);

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

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateProductByIdAsync([FromRoute] int id,
        [FromBody] UpdateProductRequest updateProductRequest)
    {
        var result = await productService.UpdateProductByIdAsync(id, updateProductRequest);
        
        if (result.Type == ServiceResponseType.Success)
        {
            return Ok();
        }
        
        if (result.Type == ServiceResponseType.NotFound)
        {
            return NotFound();
        }
        
        if (result.Type == ServiceResponseType.ValidationError)
        {
            return BadRequest(result.Message);
        }

        if (result.Type == ServiceResponseType.Conflict)
        {
            return Conflict(result.Message);
        }
        
        return StatusCode(500);
    }
}