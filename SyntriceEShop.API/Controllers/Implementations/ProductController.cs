
using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase, IProductController
{
    
}