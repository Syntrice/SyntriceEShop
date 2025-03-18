using SyntriceEShop.API.Models.ProductModel.DTO;

namespace SyntriceEShop.API.Services.Interfaces;

public interface IProductService
{
    Task<ServiceObjectResponse<IEnumerable<GetProductResponse>>> GetAllProductsAsync();
    Task<ServiceObjectResponse<GetProductResponse>> GetProductByIdAsync(int id);
    Task<ServiceObjectResponse<int>> AddProductAsync(AddProductRequest addProductRequest);
    Task<ServiceResponse> DeleteProductByIdAsync(int id);
    Task<ServiceResponse> UpdateProductAsync(int id, UpdateProductRequest updateProductRequest);
}