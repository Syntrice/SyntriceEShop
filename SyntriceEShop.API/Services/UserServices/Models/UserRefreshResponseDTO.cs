namespace SyntriceEShop.API.Services.UserServices.Models;

public class UserRefreshResponseDTO
{
    public string AccessToken { get; set; } = String.Empty;
    public string RefreshToken { get; set; } = String.Empty;
}