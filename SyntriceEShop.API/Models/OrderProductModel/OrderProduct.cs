using SyntriceEShop.API.Models.OrderModel;
using SyntriceEShop.API.Models.ProductModel;

namespace SyntriceEShop.API.Models.OrderProductModel;

public class OrderProduct 
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}