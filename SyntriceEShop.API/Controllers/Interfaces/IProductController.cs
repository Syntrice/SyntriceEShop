using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Models.ProductModel.DTO;

namespace SyntriceEShop.API.Controllers.Interfaces;

public interface IProductController
{
    Task<IActionResult> GetAllProductsAsync();
    Task<IActionResult> GetProductByIdAsync([FromRoute] int id);
    Task<IActionResult> AddProductAsync([FromBody] AddProductRequest updateProductRequest);
    Task<IActionResult> DeleteProductAsync([FromRoute] int id);
    Task<IActionResult> UpdateProductAsync([FromRoute] int id, [FromBody] UpdateProductRequest updateProductRequest);
}