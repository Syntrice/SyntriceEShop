using SyntriceEShop.API.Models.ProductModel.DTO;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Services.Implementations;

public class ProductService : IProductService
{
    public async Task<ServiceObjectResponse<IEnumerable<GetProductResponse>>> GetAllProductsAsync()
    {
        return new ServiceObjectResponse<IEnumerable<GetProductResponse>>();
    }
    
    public async Task<ServiceObjectResponse<GetProductResponse>> GetProductByIdAsync(int id)
    {
        return new ServiceObjectResponse<GetProductResponse>();
    }
    
    public async Task<ServiceObjectResponse<int>> AddProductAsync(AddProductRequest addProductRequest)
    {
        return new ServiceObjectResponse<int>();
    }
    
    public async Task<ServiceResponse> DeleteProductByIdAsync(int id)
    {
        return new ServiceResponse();
    }

    public async Task<ServiceResponse> UpdateProductByIdAsync(int id, UpdateProductRequest updateProductRequest)
    {
        return new ServiceResponse();
    }
}