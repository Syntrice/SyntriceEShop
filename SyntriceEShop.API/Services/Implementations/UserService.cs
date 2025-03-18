using SyntriceEShop.API.Models.ProductModel.DTO;
using SyntriceEShop.API.Models.ShoppingCartModel.DTO;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Services.Implementations;

public class UserService : IUserService
{
    public async Task<ServiceObjectResponse<IEnumerable<GetProductResponse>>> GetAllProductsByUserAsync()
    {
        return new ServiceObjectResponse<IEnumerable<GetProductResponse>>();
    }

    public async Task<ServiceObjectResponse<GetProductResponse>> GetAllOrdersByUserAsync()
    {
        return new ServiceObjectResponse<GetProductResponse>();
    }

    public async Task<ServiceObjectResponse<GetShoppingCartResponse>> GetShoppingCartByUserAsync()
    {
        return new ServiceObjectResponse<GetShoppingCartResponse>();
    }
}