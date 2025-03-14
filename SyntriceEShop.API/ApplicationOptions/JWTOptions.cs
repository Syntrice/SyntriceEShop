using System.ComponentModel.DataAnnotations;

namespace SyntriceEShop.API.ApplicationOptions;

public class JWTOptions
{
    public const string SectionName = "JWT";

    [Required]
    [MinLength(32)] // should be minimum of 256 bits to satisfy algorithm requirements
    public string SecretKey { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string Issuer { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    public string Audience { get; set; } = string.Empty;

    [Required] 
    [Range(1, int.MaxValue)]
    public int ExpirationInMinutes { get; set; } = 0;
}