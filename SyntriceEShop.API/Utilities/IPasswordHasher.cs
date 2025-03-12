namespace SyntriceEShop.API.Utilities;

public interface IPasswordHasher
{ 
    string Hash(string password);
    bool Verify(string password, string userPasswordHash);
}