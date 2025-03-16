using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.Common.Models;

namespace SyntriceEShop.API.Models.ShoppingCartModel;

public class ShoppingCart : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}