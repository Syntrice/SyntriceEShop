using SyntriceEShop.API.Models.ProductModel;

namespace SyntriceEShop.API.Models.ProductCategoryModel;

public class ProductCategory : IHasId
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Navigation properties
    public IEnumerable<Product>? Products { get; set; } = [];
}