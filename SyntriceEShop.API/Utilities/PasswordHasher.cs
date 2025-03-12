using System.Security.Cryptography;

namespace SyntriceEShop.API.Utilities;

public class PasswordHasher : IPasswordHasher
{

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
        
        // no hashing for now
        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }
}