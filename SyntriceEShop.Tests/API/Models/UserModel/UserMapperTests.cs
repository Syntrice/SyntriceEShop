using Shouldly;
using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.Tests.API.Models.UserModel;

[TestFixture]
public class UserMapperTests 
{
    public class UserToGetUserResponse : UserMapperTests
    {
        [Test]
        public void MapsDirectMappingsCorrectly()
        {
            // Arrange
            var input = new User()
            {
                Id = 99,
                Username = "name"
            };
            
            // Act
            var result = input.ToGetUserResponse();

            // Assert
            result.Username.ShouldBe(input.Username);
            result.Id.ShouldBe(input.Id);
        }
    }
}