using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/product-category")]
public class ProductCategoryController : ControllerBase, IProductCategoryController
{
    [HttpGet]
    public async Task<IActionResult> GetAllProductCategoriesAsync()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetProductCategoryByIdAsync([FromRoute] int id)
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("{id:int}/products")]
    public async Task<IActionResult> GetProductsByCategoryIdAsync([FromRoute] int id)
    {
        return Ok();
    }
}