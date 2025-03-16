using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.Common.Models;

namespace SyntriceEShop.API.Models.OrderModel;

public class Order : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public decimal TotalPrice { get; set; }
    public DateTime CreatedOnUTC { get; set; }
    
    // Navigation properties
    public User User { get; set; } = null!;

}