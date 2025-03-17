using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/product-category")]
public class ProductCategoryController : ControllerBase, IProductCategoryController
{
    
}