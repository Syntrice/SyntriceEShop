using SyntriceEShop.API.Models.ProductCategoryModel.DTO;

namespace SyntriceEShop.API.Models.ProductCategoryModel;

public static class ProductCategoryMapper
{
    public static GetProductCategoryResponse ToGetProductCategoryResponse(this ProductCategory productCategory)
    {
        return new GetProductCategoryResponse()
        {
            Id = productCategory.Id,
            Name = productCategory.Name
        };
    }
}