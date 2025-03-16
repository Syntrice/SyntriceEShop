using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Models.RefreshTokenModel;

public class RefreshToken : IHasGUID
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresOnUTC { get; set; }
    public User User { get; set; } = null!;
}