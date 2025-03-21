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
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public void MapsId(int id)
        {
            // Arrange
            var input = new ProductCategory()
            {
                Id = id,    
                Name = "name"
            };
            
            // Act
            var result = input.ToGetProductCategoryResponse();
            
            // Assert
            result.Id.ShouldBe(id);
        }

        [TestCase("name")]
        [TestCase("superlongname")]
        [TestCase("!%$&3493Name")]
        public void MapsName(string name)
        {
            // Arrange
            var input = new ProductCategory()
            {
                Id = 1, 
                Name = name
            };
            
            // Act                                                                                                                                                                  
            var result = input.ToGetProductCategoryResponse();
            
            // Assert
            result.Name.ShouldBe(name);
        }
    }
}