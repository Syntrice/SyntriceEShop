using System.Text;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.ApplicationOptions;
using SyntriceEShop.API.Services.UserServices;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.Tests.API.Services.UserServices;

[TestFixture]
public class JWTProviderTests
{
    private JWTProvider _jwtProvider;
    private IOptions<JWTOptions> _options;
    
    [SetUp]
    public void Setup()
    {
        _options = Substitute.For<IOptions<JWTOptions>>();
        _options.Value.Returns(GetDefaultJWTOptions());
        _jwtProvider = new JWTProvider(_options);
    }

    public static JWTOptions GetDefaultJWTOptions()
    {
        return new JWTOptions()
        {
            SecretKey = "d6bW9fMV3tCx7FAZpxc5doDpIRbWkxSk", Issuer = "automated", Audience = "test",
            ExpirationInMinutes = 30
        };
    }
    
    [TestFixture]
    public class GenerateToken : JWTProviderTests
    {
        [Test]
        public void TokenShouldNotBeNullOrEmpty()
        {
            // Arrange
            var user = new User() { Id = 1, Username = "username" };
            
            // Act
            var token = _jwtProvider.GenerateToken(user);
            
            // Assert
            token.ShouldNotBeNullOrEmpty();
        }
        
        [Test]
        public void TokenShouldBeValidFormat()
        {
            // Arrange
            var user = new User() { Id = 1, Username = "username" };
            
            // Act
            var token = _jwtProvider.GenerateToken(user);
            
            // Assert
            token.Split('.').Length.ShouldBe(3);
        }
        
        [TestCase(1)]
        [TestCase(545248)]
        [TestCase(584)]
        public void WithUserEntity_MapsSubToUserId(int id)
        {
            // Arrange
            var user = new User() { Id = id, Username = "username" };
            
            // Act
            var token = _jwtProvider.GenerateToken(user);
            
            // Assert
            var encodedPayload = token.Split('.')[1].Trim();
            if (encodedPayload.Length % 4 != 0) // If not a multiple of 4, padding is needed (= char)
            {
                encodedPayload += new string('=', 4 - encodedPayload.Length % 4);
            }
            var payloadBytes = Convert.FromBase64String(encodedPayload);
            var payload = Encoding.UTF8.GetString(payloadBytes);
            var payloadJson = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(payload);

            payloadJson["sub"].ToString().ShouldBe(user.Id.ToString());
        }

        [TestCase("ThisIsALongUsername")]
        [TestCase("AnotherLongUsername")]
        [TestCase("username123")]
        public void WithUserEntity_MapsUsername(string username)
        {
            // Arrange
            var user = new User() { Id = 1, Username = username };
            
            // Act
            var token = _jwtProvider.GenerateToken(user);
            
            // Assert
            var encodedPayload = token.Split('.')[1].Trim();
            if (encodedPayload.Length % 4 != 0) // If not a multiple of 4, padding is needed (= char)
            {
                encodedPayload += new string('=', 4 - encodedPayload.Length % 4);
            }
            var payloadBytes = Convert.FromBase64String(encodedPayload);
            var payload = Encoding.UTF8.GetString(payloadBytes);
            var payloadJson = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(payload);

            payloadJson["username"].ToString().ShouldBe(user.Username);
        }
        
        [TestCase("issuer123")]
        [TestCase("thisisalongissuer")]
        [TestCase("ThisIsAnEvenLongerIssuer")]
        public void MapsOptionsIssuer(string issuer)
        {
            // Arrange
            var user = new User() { Id = 1, Username = "username" };
            var jwtOptions = GetDefaultJWTOptions();
            jwtOptions.Issuer = issuer;
            var options = Substitute.For<IOptions<JWTOptions>>();
            options.Value.Returns(jwtOptions);
            _jwtProvider = new JWTProvider(options);
            
            // Act
            var token = _jwtProvider.GenerateToken(user);
            
            // Assert
            var encodedPayload = token.Split('.')[1].Trim();
            if (encodedPayload.Length % 4 != 0) // If not a multiple of 4, padding is needed (= char)
            {
                encodedPayload += new string('=', 4 - encodedPayload.Length % 4);
            }
            var payloadBytes = Convert.FromBase64String(encodedPayload);
            var payload = Encoding.UTF8.GetString(payloadBytes);
            var payloadJson = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(payload);

            payloadJson["iss"].ToString().ShouldBe(issuer);
        }
        
        [TestCase("thisisalongaudience")]
        [TestCase("CamalCasedAudience")]
        [TestCase("audienceWithNumbers123")]
        public void MapsOptionsAudience(string audience)
        {
            // Arrange
            var user = new User() { Id = 1, Username = "username" };
            var jwtOptions = GetDefaultJWTOptions();
            jwtOptions.Audience = audience;
            var options = Substitute.For<IOptions<JWTOptions>>();
            options.Value.Returns(jwtOptions);
            _jwtProvider = new JWTProvider(options);
            
            // Act
            var token = _jwtProvider.GenerateToken(user);
            
            // Assert
            var encodedPayload = token.Split('.')[1].Trim();
            if (encodedPayload.Length % 4 != 0) // If not a multiple of 4, padding is needed (= char)
            {
                encodedPayload += new string('=', 4 - encodedPayload.Length % 4);
            }
            var payloadBytes = Convert.FromBase64String(encodedPayload);
            var payload = Encoding.UTF8.GetString(payloadBytes);
            var payloadJson = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(payload);

            payloadJson["aud"].ToString().ShouldBe(audience);
        }
    }
}