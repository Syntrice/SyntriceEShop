using System.Security.Cryptography;

namespace SyntriceEShop.API.Utilities;

public class PasswordHasher : IPasswordHasher
{
    // These parameters might be good to be included in the DB, so that they can be changed without affecting password
    // verification
    
    private const int SaltSize = 16; // recommended size 128 bits, so 16 bytes
    private const int HashSize = 32; // recommended size 256 bits, so 32 bytes
    private const int Iterations = 100000; // Number of iterations to run the hash function
    
    // Use the SHA512 algorithm, already built into .NET
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512; 
    
    // Use RandomNumberGenerator from System.Security.Cryptography to generate salt value
    private static byte[] GetSalt() => RandomNumberGenerator.GetBytes(SaltSize);
    
    public string Hash(string password)
    {
        byte[] salt = GetSalt();
        
        // Rfc2898DeriveBytes.PBkdf2 is used to generate keys through a password, salt, and iteration count
        // PBkdf2: Password-based-key-derivation-function, described in Rfc2898 standard
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
        
        // passwords are stored as "[passwordhash]-[salt]" format
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Verify(string password, string userPasswordHash)
    {
        string[] parts = userPasswordHash.Split("-"); // passwords are stored as "[passwordhash]-[salt]" format
        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);
        
        // Hash the incoming password
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
        
        // to avoid timing attacks (where the hacker can use information about how long the algorithm takes to narrow down search)
        // it is best to use a CryptographicOperations method
        
        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}