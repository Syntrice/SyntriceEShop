using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Models.ProductModel.DTO;

namespace SyntriceEShop.API.Controllers.Interfaces;

public interface IProductController
{
    Task<IActionResult> GetAllProductsAsync();
    Task<IActionResult> GetProductByIdAsync([FromRoute] int id);
    Task<IActionResult> AddProductAsync([FromBody] AddProductRequest addProductRequest);
    Task<IActionResult> DeleteProductByIdAsync([FromRoute] int id);
    Task<IActionResult> UpdateProductByIdAsync([FromRoute] int id, [FromBody] UpdateProductRequest updateProductRequest);
}