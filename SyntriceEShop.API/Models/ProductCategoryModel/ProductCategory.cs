using SyntriceEShop.Common.Models;

namespace SyntriceEShop.API.Models.ProductCategoryModel;

public class ProductCategory : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}