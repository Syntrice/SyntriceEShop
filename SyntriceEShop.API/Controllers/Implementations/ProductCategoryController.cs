using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/product-category")]
public class ProductCategoryController(IProductCategoryService productCategoryService)
    : ControllerBase, IProductCategoryController
{
    [HttpGet]
    public async Task<IActionResult> GetAllProductCategoriesAsync()
    {
        var result = await productCategoryService.GetAllProductCategoriesAsync();

        if (result.Type == ServiceResponseType.Success)
        {
            return Ok(result.Value);
        }

        return StatusCode(500);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetProductCategoryByIdAsync([FromRoute] int id)
    {
        var result = await productCategoryService.GetProductCategoryByIdAsync(id);

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

    [HttpGet]
    [Route("{id:int}/products")]
    public async Task<IActionResult> GetProductsByCategoryIdAsync([FromRoute] int id)
    {
        var result = await productCategoryService.GetProductsByCategoryIdAsync(id);
        
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
}