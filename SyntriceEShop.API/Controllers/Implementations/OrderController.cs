using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase, IOrderController
{
    
}