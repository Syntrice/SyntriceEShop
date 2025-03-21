namespace SyntriceEShop.API.Models.OrderModel.DTO;

public class GetOrderResponse
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public decimal TotalPrice { get; set; }
    public DateTime CreatedOnUTC { get; set; }
}