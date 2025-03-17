namespace SyntriceEShop.API.Models.AuthModel.DTO;

public class AuthLoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}