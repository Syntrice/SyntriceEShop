using SyntriceEShop.API.Models.ProductCategoryModel.DTO;
using SyntriceEShop.API.Models.ProductModel.DTO;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Services.Implementations;

public class ProductCategoryService : IProductCategoryService
{
    public async Task<ServiceObjectResponse<IEnumerable<GetProductCategoryResponse>>> GetAllProductCategoriesAsync()
    {
        return new ServiceObjectResponse<IEnumerable<GetProductCategoryResponse>>();
    }
    
    public async Task<ServiceObjectResponse<GetProductCategoryResponse>> GetProductCategoryByIdAsync(int id)
    {
        return new ServiceObjectResponse<GetProductCategoryResponse>();
    }
    
    public async Task<ServiceObjectResponse<IEnumerable<GetProductResponse>>> GetProductsByCategoryIdAsync(int id)
    {
        return new ServiceObjectResponse<IEnumerable<GetProductResponse>>();
    }
}