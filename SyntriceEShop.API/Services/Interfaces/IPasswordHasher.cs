namespace SyntriceEShop.API.Services.Interfaces;

public interface IPasswordHasher
{ 
    string Hash(string password);
    bool Verify(string password, string userPasswordHash);
}