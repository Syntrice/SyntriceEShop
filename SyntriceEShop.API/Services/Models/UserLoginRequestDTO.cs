namespace SyntriceEShop.API.Services.Models;

public class UserLoginRequestDTO
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}