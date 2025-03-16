using SyntriceEShop.API.Models.OrderModel;
using SyntriceEShop.API.Models.ShoppingCartModel;
using SyntriceEShop.Common.Models;

namespace SyntriceEShop.API.Models.UserModel;

/// <summary>
/// Entity for application user
/// </summary>
public class User : IEntity
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    // Navigation properties
    public IEnumerable<Order> Orders { get; set; } = [];
    public ShoppingCart? ShoppingCart { get; set; }
}