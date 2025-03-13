namespace SyntriceEShop.API.Services.UserServices;

public interface IPasswordHasher
{ 
    string Hash(string password);
    bool Verify(string password, string userPasswordHash);
}