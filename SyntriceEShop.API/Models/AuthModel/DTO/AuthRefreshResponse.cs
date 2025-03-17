namespace SyntriceEShop.API.Models.AuthModel.DTO;

public class AuthRefreshResponse
{
    public string AccessToken { get; set; } = String.Empty;
    public string RefreshToken { get; set; } = String.Empty;
}