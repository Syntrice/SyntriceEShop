namespace SyntriceEShop.API.Models.UserModel;

public class UserLoginResponseDTO
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}