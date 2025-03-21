using Shouldly;
using SyntriceEShop.API.Models.OrderModel;
using SyntriceEShop.API.Models.OrderModel.DTO;

namespace SyntriceEShop.Tests.API.Models.OrderModel;

[TestFixture]
public class OrderMapperTests
{
    [TestFixture]
    public class AddOrderRequestToOrder : OrderMapperTests
    {
        [Test]
        public void MapsDirectMappingsCorrectly()
        {
            // Arrange
            var input = new AddOrderRequest()
            {
                UserId = 99,
                TotalPrice = 156_00,
                CreatedOnUTC = DateTime.UtcNow
            };
            
            // Act
            var result = input.ToOrder();
            
            // Assert
            result.UserId.ShouldBe(input.UserId);
            result.TotalPrice.ShouldBe(input.TotalPrice);
            result.CreatedOnUTC.ShouldBe(input.CreatedOnUTC);
        }
    }

    [TestFixture]
    public class OrderToGetOrderResponse : OrderMapperTests
    {
        [Test]
        public void MapsDirectMappingsCorrectly()
        {
            // Arrange
            var input = new Order()
            {
                Id = 77,   
                UserId = 99,
                TotalPrice = 156_00,
                CreatedOnUTC = DateTime.UtcNow
            };
            
            // Act
            var result = input.ToGetOrderResponse();
            
            // Assert
            result.Id.ShouldBe(input.Id);
            result.UserId.ShouldBe(input.UserId);
            result.TotalPrice.ShouldBe(input.TotalPrice);
            result.CreatedOnUTC.ShouldBe(input.CreatedOnUTC);
        }
    }
}