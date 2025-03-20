using SyntriceEShop.API.Models.OrderModel.DTO;
using SyntriceEShop.API.Models.ProductModel.DTO;
using SyntriceEShop.API.Models.ShoppingCartModel.DTO;

namespace SyntriceEShop.API.Services.Interfaces;

public interface IUserService
{
    Task<ServiceObjectResponse<IEnumerable<GetProductResponse>>> GetAllProductsByUserAsync(int userId);
    Task<ServiceObjectResponse<IEnumerable<GetOrderResponse>>> GetAllOrdersByUserAsync(int userId);
    Task<ServiceObjectResponse<GetShoppingCartResponse>> GetShoppingCartByUserAsync(int userId);
}