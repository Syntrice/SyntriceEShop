
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;
using SyntriceEShop.API.Models.ProductModel.DTO;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase, IProductController
{
    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetProductByIdAsync([FromRoute] int id)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddProductAsync([FromBody] AddProductRequest updateProductRequest)
    {
        return Ok();
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
    {
        return Ok();
    }
    
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateProductAsync([FromRoute] int id, [FromBody] UpdateProductRequest updateProductRequest)
    {
        return Ok();
    }
}