using SyntriceEShop.API.Models.ProductCategoryModel;
using SyntriceEShop.Common.Models;

namespace SyntriceEShop.API.Models.ProductModel;

public class Product : IEntity
{
    public int Id { get; set; }
    
    public int ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; } = null!;
    
    public decimal Price { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
}