using SyntriceEShop.API.Models.ProductCategoryModel.DTO;
using SyntriceEShop.API.Models.ProductModel.DTO;

namespace SyntriceEShop.API.Services.Interfaces;

public interface IProductCategoryService
{
    Task<ServiceObjectResponse<IEnumerable<GetProductCategoryResponse>>> GetAllProductCategoriesAsync();
    Task<ServiceObjectResponse<GetProductCategoryResponse>> GetProductCategoryByIdAsync(int id);
    Task<ServiceObjectResponse<IEnumerable<GetProductResponse>>> GetProductsByCategoryIdAsync(int id);
}