using SyntriceEShop.API.Models.ProductCategoryModel;

namespace SyntriceEShop.API.Models.ProductModel;

public class Product : IEntity<int>
{
    public int Id { get; set; }
    public int ProductCategoryId { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
    public int UserId { get; set; }
    
    // Navigation properties
    public ProductCategory ProductCategory { get; set; } = null!;
}