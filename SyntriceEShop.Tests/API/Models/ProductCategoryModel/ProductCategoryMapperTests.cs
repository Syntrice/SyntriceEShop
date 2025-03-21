using Shouldly;
using SyntriceEShop.API.Models.ProductCategoryModel;
using SyntriceEShop.API.Models.ProductModel;

namespace SyntriceEShop.Tests.API.Models.ProductCategoryModel;

[TestFixture]
public class ProductCategoryMapperTests
{
    [TestFixture]
    public class ProductCategoryToGetProductCategoryResponse : ProductCategoryMapperTests
    {
        [Test]
        public void MapsDirectMappingsCorrectly()
        {
            // Arrange
            var input = new ProductCategory()
            {
                Id = 99,
                Name = "name"
            };
            
            // Act
            var result = input.ToGetProductCategoryResponse();

            // Assert
            result.Name.ShouldBe(input.Name);
            result.Id.ShouldBe(input.Id);
        }
    }
}