namespace SyntriceEShop.API.Services.UserServices.Models;

public class UserLoginResponseDTO
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}