using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Controllers.Interfaces;

namespace SyntriceEShop.API.Controllers.Implementations;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase, IUserController
{
    
}