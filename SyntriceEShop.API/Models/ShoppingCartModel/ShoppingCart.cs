using SyntriceEShop.API.Models.OrderProductModel;
using SyntriceEShop.API.Models.ProductModel;
using SyntriceEShop.API.Models.ShoppingCartProductModel;
using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Models.ShoppingCartModel;

public class ShoppingCart : IHasId
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;
    public IEnumerable<Product> Products { get; } = [];
    public IEnumerable<ShoppingCartProduct> ShoppingCartProducts { get; } = [];
}