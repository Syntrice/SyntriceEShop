namespace SyntriceEShop.API.Utility;

public interface IPasswordHasher
{ 
    string Hash(string password);
}