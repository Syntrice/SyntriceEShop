namespace SyntriceEShop.API.Models.UserModel.DTO;

public class UserRefreshResponse
{
    public string AccessToken { get; set; } = String.Empty;
    public string RefreshToken { get; set; } = String.Empty;
}