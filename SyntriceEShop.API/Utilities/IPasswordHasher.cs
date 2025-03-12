namespace SyntriceEShop.API.Utilities;

public interface IPasswordHasher
{ 
    string Hash(string password);
}