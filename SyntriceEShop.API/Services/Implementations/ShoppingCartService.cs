using Microsoft.AspNetCore.Mvc;
using SyntriceEShop.API.Models.ShoppingCartModel.DTO;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Services.Implementations;

public class ShoppingCartService : IShoppingCartService
{
    
    public async Task<ServiceObjectResponse<int>> AddShoppingCartAsync(AddShoppingCartRequest addShoppingCartRequest)
    {
        return new ServiceObjectResponse<int>();
    }
    public async Task<ServiceResponse> DeleteShoppingCartAsync(int id)
    {
        return new ServiceResponse();
    }
    public async Task<ServiceResponse> UpdateShoppingCartByIdAsync(int id, UpdateShoppingCartRequest updateShoppingCartRequest)
    {
        return new ServiceResponse();
    }
} 