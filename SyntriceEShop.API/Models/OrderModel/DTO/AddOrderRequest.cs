namespace SyntriceEShop.API.Models.OrderModel.DTO;

public class AddOrderRequest
{
    public int UserId { get; set; } 
    public decimal TotalPrice { get; set; }
    public DateTime CreatedOnUTC { get; set; }
}