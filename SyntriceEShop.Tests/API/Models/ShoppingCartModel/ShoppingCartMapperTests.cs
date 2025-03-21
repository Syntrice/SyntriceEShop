using Shouldly;
using SyntriceEShop.API.Models.ShoppingCartModel;
using SyntriceEShop.API.Models.ShoppingCartModel.DTO;

namespace SyntriceEShop.Tests.API.Models.ShoppingCartModel;

[TestFixture]
public class ShoppingCartMapperTests
{
    [TestFixture]
    public class AddShoppingCartRequestToShoppingCart : ShoppingCartMapperTests
    {
        [Test]
        public void MapsDirectMappingsCorrectly()
        {
            // Arrange
            var input = new AddShoppingCartRequest()
            {
                UserId = 99
            };
            
            // Act
            var result = input.ToShoppingCart();
            
            // Assert
            result.UserId.ShouldBe(input.UserId);
        }
    }

    [TestFixture]
    public class UpdateShoppingCartRequestToShoppingCart : ShoppingCartMapperTests
    {
        [Test]
        public void MapsDirectMappingsCorrectly()
        {
            // Arrange
            var input = new UpdateShoppingCartRequest()
            {
                UserId = 99
            };
            
            // Act
            var result = input.ToShoppingCart();
            
            // Assert
            result.UserId.ShouldBe(input.UserId);
        }
    }

    [TestFixture]
    public class ShoppingCartToGetShoppingCartResponse : ShoppingCartMapperTests
    {
        [Test]
        public void MapsDirectMappingsCorrectly()
        {
            // Arrange
            var input = new ShoppingCart()
            {
                UserId = 99
            };

            // Act
            var result = input.ToGetShoppingCartResponse();

            // Assert
            result.UserId.ShouldBe(input.UserId);
        }
    }
}