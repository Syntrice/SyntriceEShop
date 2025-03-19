using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.Controllers.Implementations;
using SyntriceEShop.API.Models.ProductCategoryModel.DTO;
using SyntriceEShop.API.Models.ProductModel.DTO;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Interfaces;
using SyntriceEShop.Tests.API.Services.UserServices;

namespace SyntriceEShop.Tests.API.Controllers.Implementations;

[TestFixture]
public class ProductCategoryControllerTests
{
    private IProductCategoryService _productCategoryService;
    private ProductCategoryController _controller;
    
    [SetUp]
    public void Setup()
    {
        _productCategoryService = Substitute.For<IProductCategoryService>();
        _controller = new ProductCategoryController(_productCategoryService);
    }
    
    [TestFixture]
    public class GetAllProductCategoriesAsync : ProductCategoryControllerTests
    {
        [Test]
        public async Task CallsProductCategoryService_GetAllProductCategoriesAsync()
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductCategoryResponse>>();
            _productCategoryService.GetAllProductCategoriesAsync().Returns(serviceResult);
            
            // Act
            var result = await _controller.GetAllProductCategoriesAsync();
            
            // Assert
            await _productCategoryService.Received(1).GetAllProductCategoriesAsync();
        }

        [Test]
        public async Task WhenProductCategoryService_ReturnsSuccess_ReturnsOkObjectResultWithProductCategories()
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductCategoryResponse>>()
            {
                Type = ServiceResponseType.Success,
                Value = [new GetProductCategoryResponse(), new GetProductCategoryResponse()]
            };
            _productCategoryService.GetAllProductCategoriesAsync().Returns(serviceResult);
            
            // Act
            var result = await _controller.GetAllProductCategoriesAsync();
            
            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as IEnumerable<GetProductCategoryResponse>;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }

        [Test]
        public async Task WhenProductCategoryService_ReturnsEmptyList_ReturnsOkObjectResultWithEmptyList()
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductCategoryResponse>>()
            {
                Type = ServiceResponseType.Success,
                Value = []
            };
            _productCategoryService.GetAllProductCategoriesAsync().Returns(serviceResult);
            
            // Act
            var result = await _controller.GetAllProductCategoriesAsync();
            
            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as IEnumerable<GetProductCategoryResponse>;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
    }

    [TestFixture]
    public class GetProductCategoryByIdAsync : ProductCategoryControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task CallsProductCategoryService_GetProductCategoryByIdAsync(int id)
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<GetProductCategoryResponse>();
            _productCategoryService.GetProductCategoryByIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetProductCategoryByIdAsync(id);
                
            // Assert
            await _productCategoryService.Received(1).GetProductCategoryByIdAsync(id);
        }
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task WhenProductCategoryService_ReturnsSuccess_ReturnsOkObjectResultWithProductCategory(int id)
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<GetProductCategoryResponse>()
            {
                Type = ServiceResponseType.Success, 
                Value = new GetProductCategoryResponse()
            };
            _productCategoryService.GetProductCategoryByIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetProductCategoryByIdAsync(id);
                
            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as GetProductCategoryResponse;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
        
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task WhenProductCategoryService_ReturnsNotFound_ReturnsNotFoundResult(int id)
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<GetProductCategoryResponse>()
            {
                Type = ServiceResponseType.NotFound,
            };
            _productCategoryService.GetProductCategoryByIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetProductCategoryByIdAsync(id);
                
            // Assert
            result.ShouldBeOfType(typeof(NotFoundResult));
        }
    }

    [TestFixture]
    public class GetProductsByCategoryIdAsync : ProductCategoryControllerTests
    {
        [Test]
        public async Task CallsProductCategoryService_GetProductsByCategoryIdAsync()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>()
            {
        
            };
            _productCategoryService.GetProductsByCategoryIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetProductsByCategoryIdAsync(id);
                
            // Assert
            await _productCategoryService.Received(1).GetProductsByCategoryIdAsync(id);
        }
        
        [Test]
        public async Task WhenProductCategoryService_ReturnsSuccess_ReturnsOkObjectResultWithProducts()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>()
            {
                Type = ServiceResponseType.Success, 
                Value = [new GetProductResponse(), new GetProductResponse()]
            };
            _productCategoryService.GetProductsByCategoryIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetProductsByCategoryIdAsync(id);
                
            // Assert
            result.ShouldBeOfType<OkObjectResult>();
            var resultValue = ((OkObjectResult) result).Value as IEnumerable<GetProductResponse>;
            resultValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
        
        [Test]
        public async Task WhenProductCategoryService_ReturnsEmptyList_ReturnsOkObjectResultWithEmptyList()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>()
            {
                Type = ServiceResponseType.Success, 
                Value = []
            };
            _productCategoryService.GetProductsByCategoryIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetProductsByCategoryIdAsync(id);
                
            // Assert
            result.ShouldBeOfType<OkObjectResult>();
            var resultValue = ((OkObjectResult) result).Value as IEnumerable<GetProductResponse>;
            resultValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
        
        
        [Test]
        public async Task WhenProductCategoryService_ReturnsNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>()
            {
                Type = ServiceResponseType.NotFound
            };
            _productCategoryService.GetProductsByCategoryIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetProductsByCategoryIdAsync(id);
                
            // Assert
            result.ShouldBeOfType<NotFoundResult>();
        }
    }
}