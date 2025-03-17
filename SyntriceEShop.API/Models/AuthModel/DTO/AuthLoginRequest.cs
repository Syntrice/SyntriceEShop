namespace SyntriceEShop.API.Models.AuthModel.DTO;

public class AuthLoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}