namespace SyntriceEShop.API.Utilities;

public class PasswordHasher : IPasswordHasher
{

    public string Hash(string password)
    {
        // no hashing for now
        return password;
    }
}