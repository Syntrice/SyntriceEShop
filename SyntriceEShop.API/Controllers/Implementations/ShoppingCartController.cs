using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/shopping-cart")]
public class ShoppingCartController : ControllerBase, IShoppingCartController
{
    
}