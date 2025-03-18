using SyntriceEShop.API.Models.ShoppingCartModel.DTO;

namespace SyntriceEShop.API.Services.Interfaces;

public interface IShoppingCartService
{
    Task<ServiceObjectResponse<int>> AddShoppingCartAsync(AddShoppingCartRequest addShoppingCartRequest);
    Task<ServiceResponse> DeleteShoppingCartAsync(int id);
    Task<ServiceResponse> UpdateShoppingCartByIdAsync(int id, UpdateShoppingCartRequest updateShoppingCartRequest);
}