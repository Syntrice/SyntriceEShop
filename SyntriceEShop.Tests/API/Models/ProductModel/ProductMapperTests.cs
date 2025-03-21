using System.Security.Cryptography.X509Certificates;
using Shouldly;
using SyntriceEShop.API.Models.ProductModel;
using SyntriceEShop.API.Models.ProductModel.DTO;

namespace SyntriceEShop.Tests.API.Models.ProductModel;

[TestFixture]
public class ProductMapperTests
{
    [TestFixture]
    public class AddProductRequestToProduct : ProductMapperTests
    {
        [Test]
        public void MapsDirectMappingsCorrectly()
        {
            // Arrange
            var input = new AddProductRequest()
            {
                Name = "name",
                Description = "this is a description",
                Price = 98_00,
                ProductCategoryId = 12,
                UserId = 56,
                QuantityInStock = 999
            };
            
            // Act
            var result = input.ToProduct();
            
            // Assert
            result.Name.ShouldBe(input.Name);
            result.Description.ShouldBe(input.Description);
            result.Price.ShouldBe(input.Price);
            result.ProductCategoryId.ShouldBe(input.ProductCategoryId);
            result.UserId.ShouldBe(input.UserId);
            result.QuantityInStock.ShouldBe(input.QuantityInStock);
        }
    }

    [TestFixture]
    public class UpdateProductRequestToProduct : ProductMapperTests
    {
        [Test]
        public void MapsDirectMappingsCorrectly()
        {
            // Arrange
            var input = new UpdateProductRequest()
            {
                Name = "name",
                Description = "this is a description",
                Price = 98_00,
                ProductCategoryId = 12,
                UserId = 56,
                QuantityInStock = 999
            };
            
            // Act
            var result = input.ToProduct();
            
            // Assert
            result.Name.ShouldBe(input.Name);
            result.Description.ShouldBe(input.Description);
            result.Price.ShouldBe(input.Price);
            result.ProductCategoryId.ShouldBe(input.ProductCategoryId);
            result.UserId.ShouldBe(input.UserId);
            result.QuantityInStock.ShouldBe(input.QuantityInStock);
        }
    }

    [TestFixture]
    public class ProductToGetProductResponse : ProductMapperTests
    {
        [Test]
        public void MapsDirectMappingsCorrectly()
        {
            // Arrange
            var input = new Product()
            {
                Id = 85,
                Name = "name",
                Description = "this is a description",
                Price = 98_00,
                ProductCategoryId = 12,
                UserId = 56,
                QuantityInStock = 999
            };
            
            // Act
            var result = input.ToGetProductResponse();
            
            // Assert
            result.Name.ShouldBe(input.Name);
            result.Description.ShouldBe(input.Description);
            result.Price.ShouldBe(input.Price);
            result.ProductCategoryId.ShouldBe(input.ProductCategoryId);
            result.UserId.ShouldBe(input.UserId);
            result.QuantityInStock.ShouldBe(input.QuantityInStock);
        }
    }
}