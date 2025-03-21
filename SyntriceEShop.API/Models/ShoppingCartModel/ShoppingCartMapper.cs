using SyntriceEShop.API.Models.ShoppingCartModel.DTO;

namespace SyntriceEShop.API.Models.ShoppingCartModel;

public static class ShoppingCartMapper
{
    public static ShoppingCart ToShoppingCart(this AddShoppingCartRequest request)
    {
        return new ShoppingCart();
    }
    
    public static ShoppingCart ToShoppingCart(this UpdateShoppingCartRequest request)
    {
        return new ShoppingCart();
    }

    public static GetShoppingCartResponse ToGetShoppingCartResponse(this ShoppingCart shoppingCart)
    {
        return new GetShoppingCartResponse();
    }
}