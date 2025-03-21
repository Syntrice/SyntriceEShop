using Shouldly;
using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.Tests.API.Models.UserModel;

[TestFixture]
public class UserMapperTests 
{
    public class UserToGetUserResponse : UserMapperTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public void MapsId(int id)
        {
            // Arrange
            var input = new User()
            {
                Id = id,    
                Username = "name"
            };
            
            // Act
            var result = input.ToGetUserResponse();
            
            // Assert
            result.Id.ShouldBe(id);
        }

        [TestCase("name")]
        [TestCase("superlongname")]
        [TestCase("!%$&3493Name")]
        public void MapsName(string username)
        {
            // Arrange
            var input = new User()
            {
                Id = 1, 
                Username = username
            };
            
            // Act                                                                                                                                                                  
            var result = input.ToGetUserResponse();
            
            // Assert
            result.Username.ShouldBe(username);
        }
    }
}