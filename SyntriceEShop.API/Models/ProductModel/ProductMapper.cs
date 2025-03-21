using Microsoft.AspNetCore.Server.HttpSys;
using SyntriceEShop.API.Models.ProductModel.DTO;

namespace SyntriceEShop.API.Models.ProductModel;

public static class ProductMapper
{
    public static Product ToProduct(this AddProductRequest request)
    {
        return new Product
        {
            Description = request.Description,
            ProductCategoryId = request.ProductCategoryId,
            Name = request.Name,
            Price = request.Price,
            UserId = request.UserId,
            QuantityInStock = request.QuantityInStock,
        };
    }

    public static Product ToProduct(this UpdateProductRequest request)
    {
        return new Product
        {
            Description = request.Description,
            ProductCategoryId = request.ProductCategoryId,
            Name = request.Name,
            Price = request.Price,
            UserId = request.UserId,
            QuantityInStock = request.QuantityInStock,
        };
    }

    public static GetProductResponse ToGetProductResponse(this Product product)
    {
        return new GetProductResponse
        {
            Id = product.Id,
            Description = product.Description,
            ProductCategoryId = product.ProductCategoryId,
            Name = product.Name,
            Price = product.Price,
            UserId = product.UserId,
            QuantityInStock = product.QuantityInStock,
        };
    }
}