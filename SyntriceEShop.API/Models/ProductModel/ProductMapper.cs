using SyntriceEShop.API.Models.ProductModel.DTO;

namespace SyntriceEShop.API.Models.ProductModel;

public static class ProductMapper
{
    public static Product ToProduct(this AddProductRequest request)
    {
        return new Product();
    }

    public static Product ToProduct(this UpdateProductRequest request)
    {
        return new Product();
    }

    public static GetProductResponse ToGetProductResponse(this Product product)
    {
        return new GetProductResponse();
    }
}