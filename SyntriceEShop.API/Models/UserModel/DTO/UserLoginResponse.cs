namespace SyntriceEShop.API.Models.UserModel.DTO;

public class UserLoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}