using SyntriceEShop.API.Models.ProductModel.DTO;
using SyntriceEShop.API.Models.ShoppingCartModel.DTO;

namespace SyntriceEShop.API.Services.Interfaces;

public interface IUserService
{
    Task<ServiceObjectResponse<IEnumerable<GetProductResponse>>> GetAllProductsByUserAsync();
    Task<ServiceObjectResponse<GetProductResponse>> GetAllOrdersByUserAsync();
    Task<ServiceObjectResponse<GetShoppingCartResponse>> GetShoppingCartByUserAsync();
}