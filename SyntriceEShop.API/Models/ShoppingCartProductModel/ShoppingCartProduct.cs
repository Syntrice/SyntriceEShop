using SyntriceEShop.API.Models.ProductModel;
using SyntriceEShop.API.Models.ShoppingCartModel;

namespace SyntriceEShop.API.Models.ShoppingCartProductModel;

public class ShoppingCartProduct
{
    public int ShoppingCartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    
    public ShoppingCart ShoppingCart { get; set; } = null!;
    public Product Product { get; set; } = null!;
}