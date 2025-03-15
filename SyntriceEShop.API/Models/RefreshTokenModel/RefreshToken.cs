using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.Common.Models;

namespace SyntriceEShop.API.Models.RefreshTokenModel;

public class RefreshToken : IGUIDEntity
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresOnUTC { get; set; }
    public User User { get; set; } = null!;
}