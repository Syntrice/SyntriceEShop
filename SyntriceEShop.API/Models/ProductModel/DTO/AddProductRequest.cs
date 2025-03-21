namespace SyntriceEShop.API.Models.ProductModel.DTO;

public class AddProductRequest
{
    public int ProductCategoryId { get; set; }
    public int UserId { get; set; }
    public decimal Price { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
}