using Microsoft.AspNetCore.Mvc;

namespace SyntriceEShop.API.Controllers.Interfaces;

public interface IProductCategoryController
{
    Task<IActionResult> GetAllProductCategoriesAsync();
    Task<IActionResult> GetProductCategoryByIdAsync([FromRoute] int id);
    Task<IActionResult> GetProductsByCategoryIdAsync([FromRoute] int id);
}