using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.Controllers.Implementations;
using SyntriceEShop.API.Models.ProductModel.DTO;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Interfaces;
using SyntriceEShop.Tests.API.Services.UserServices;

namespace SyntriceEShop.Tests.API.Controllers.Implementations;

[TestFixture]
public class ProductControllerTests
{
    private ProductController _controller;
    private IProductService _productService;
    
    [SetUp]
    public void Setup()
    {
        _productService = Substitute.For<IProductService>();
        _controller = new ProductController(_productService);
    }

    [TestFixture]
    public class GetAllProductsAsync : ProductControllerTests
    {
        [Test]
        public async Task CallsProductService_GetAllProductsAsync()
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>();
            _productService.GetAllProductsAsync().Returns(serviceResult);
            
            // Act
            await _controller.GetAllProductsAsync();
            
            // Assert
            await _productService.Received(1).GetAllProductsAsync();
        }

        [Test]
        public async Task WhenProductService_ReturnsSuccessful_ReturnsOkObjectResultWithValues()
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>()
            {
                Type = ServiceResponseType.Success,
                Value = [new GetProductResponse(), new GetProductResponse()]
            };
            _productService.GetAllProductsAsync().Returns(serviceResult);
            
            // Act
            var result = await _controller.GetAllProductsAsync();
            
            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as IEnumerable<GetProductResponse>;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }

        [Test]
        public async Task WhenProductService_ReturnsEmptyList_ReturnsOkObjectResultWithEmptyList()
        {
            
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>()
            {
                Type = ServiceResponseType.Success,
                Value = []
            };
            _productService.GetAllProductsAsync().Returns(serviceResult);
            
            // Act
            var result = await _controller.GetAllProductsAsync();
            
            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as IEnumerable<GetProductResponse>;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
    }

    [TestFixture]
    public class GetProductByIdAsync : ProductControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task CallsProductService_GetProductByIdAsync(int id)
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<GetProductResponse>();
            _productService.GetProductByIdAsync(id).Returns(serviceResult);
            
            // Act
            await _controller.GetProductByIdAsync(id);
            
            // Assert
            await _productService.Received(1).GetProductByIdAsync(id);
        }
        
        [Test]
        public async Task WhenProductService_ReturnsSuccess_ReturnsOkObjectResultWithValue()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<GetProductResponse>()
            {
                Type = ServiceResponseType.Success,
                Value = new GetProductResponse()
            };
            _productService.GetProductByIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetProductByIdAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as GetProductResponse;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
        
        
        [Test]
        public async Task WhenProductService_ReturnsNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<GetProductResponse>()
            {
                Type = ServiceResponseType.NotFound,
            };
            _productService.GetProductByIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetProductByIdAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(NotFoundResult));
        }
    }

    [TestFixture]
    public class AddProductAsync : ProductControllerTests
    {
        [Test]
        public async Task CallsProductService_AddProductAsync()
        {
            // Arrange
            var addProductRequest = new AddProductRequest();
            var serviceResult = new ServiceObjectResponse<int>();
            _productService.AddProductAsync(addProductRequest).Returns(serviceResult);
            
            // Act
            await _controller.AddProductAsync(addProductRequest);
            
            // Assert
            await _productService.Received(1).AddProductAsync(addProductRequest);
        }
        
        [Test]
        public async Task WhenProductService_ReturnsSuccess_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            int id = 1;
            var addProductRequest = new AddProductRequest();
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.Success,
                Value = id,
                Message = "message"
            };
            _productService.AddProductAsync(addProductRequest).Returns(serviceResult);
            
            // Act
            var result = await _controller.AddProductAsync(addProductRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(CreatedAtRouteResult));
        }

        [Test]
        public async Task WhenProductService_ReturnsConflict_ReturnsConflictObjectResultWithMessage()
        {
            // Arrange
            int id = 1;
            var addProductRequest = new AddProductRequest();
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.Conflict,
                Value = id
            };
            _productService.AddProductAsync(addProductRequest).Returns(serviceResult);
            
            // Act
            var result = await _controller.AddProductAsync(addProductRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(ConflictObjectResult));
            var responseValue = (result as ConflictObjectResult)?.Value as string;
            responseValue.ShouldBeEquivalentTo(serviceResult.Message);
        }
        
        [Test]
        public async Task WhenProductService_ReturnsValidationError_ReturnsBadRequestObjectResultWithMessage()
        {
            // Arrange
            int id = 1;
            var addProductRequest = new AddProductRequest();
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.ValidationError,
                Value = id
            };
            _productService.AddProductAsync(addProductRequest).Returns(serviceResult);
            
            // Act
            var result = await _controller.AddProductAsync(addProductRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(BadRequestObjectResult));
            var responseValue = (result as BadRequestObjectResult)?.Value as string;
            responseValue.ShouldBeEquivalentTo(serviceResult.Message);
        }
    }

    [TestFixture]
    public class DeleteProductByIdAsync : ProductControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task CallsProductService_DeleteProductByIdAsync(int id)
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<int>();
            _productService.DeleteProductByIdAsync(id).Returns(serviceResult);
            
            // Act
            await _controller.DeleteProductByIdAsync(id);
            
            // Assert
            await _productService.Received(1).DeleteProductByIdAsync(id);
        }
        
        [Test]
        public async Task WhenProductService_ReturnsSuccess_ReturnsNoContentResult()
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.Success,
            };
            _productService.DeleteProductByIdAsync(1).Returns(serviceResult);
            
            // Act
            var result = await _controller.DeleteProductByIdAsync(1);
            
            // Assert
            result.ShouldBeOfType(typeof(NoContentResult));
        }
        
        [Test]
        public async Task WhenProductService_ReturnsNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.NotFound,
            };
            _productService.DeleteProductByIdAsync(1).Returns(serviceResult);
            
            // Act
            var result = await _controller.DeleteProductByIdAsync(1);
            
            // Assert
            result.ShouldBeOfType(typeof(NotFoundResult));
        }
    }

    [TestFixture]
    public class UpdateProductAsync : ProductControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task CallsProductService_UpdateProductByIdAsync(int id)
        {
            // Arrange
            var updateProductRequest = new UpdateProductRequest();
            var serviceResult = new ServiceObjectResponse<int>();
            _productService.UpdateProductByIdAsync(id, updateProductRequest).Returns(serviceResult);

            // Act
            await _controller.UpdateProductByIdAsync(id, updateProductRequest);

            // Assert
            await _productService.Received(1).UpdateProductByIdAsync(id, updateProductRequest);
        }

        [Test]
        public async Task WhenProductService_ReturnsSuccess_ReturnsNoContentResult()
        {
            // Arrange
            int id = 1;
            var updateProductRequest = new UpdateProductRequest();
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.Success,
            };
            _productService.UpdateProductByIdAsync(id, updateProductRequest).Returns(serviceResult);

            // Act
            var result = await _controller.UpdateProductByIdAsync(id, updateProductRequest);

            // Assert
            result.ShouldBeOfType(typeof(NoContentResult));
        }
        
        [Test]
        public async Task WhenProductService_ReturnsNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            int id = 1;
            var updateProductRequest = new UpdateProductRequest();
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.NotFound,
            };
            _productService.UpdateProductByIdAsync(id, updateProductRequest).Returns(serviceResult);
            
            // Act
            var result = await _controller.UpdateProductByIdAsync(id, updateProductRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(NotFoundResult));
        }
        
        [Test]
        public async Task WhenProductService_ReturnsValidationError_ReturnsBadRequestObjectResultWithMessage()
        {
            // Arrange
            int id = 1;
            var updateProductRequest = new UpdateProductRequest();
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.ValidationError,
            };
            _productService.UpdateProductByIdAsync(id, updateProductRequest).Returns(serviceResult);
            
            // Act
            var result = await _controller.UpdateProductByIdAsync(id, updateProductRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(BadRequestObjectResult));
            var responseValue = (result as BadRequestObjectResult)?.Value as string;
            responseValue.ShouldBeEquivalentTo(serviceResult.Message);
        }
        
        [Test]
        public async Task WhenProductService_ReturnsConflict_ReturnsConflictObjectResultWithMessage()
        {
            // Arrange
            int id = 1;
            var updateProductRequest = new UpdateProductRequest();
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.Conflict,
                Message = "message"
            };
            _productService.UpdateProductByIdAsync(id, updateProductRequest).Returns(serviceResult);
            
            // Actg
            var result = await _controller.UpdateProductByIdAsync(id, updateProductRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(ConflictObjectResult));
            var responseValue = (result as ConflictObjectResult)?.Value as string;
            responseValue.ShouldBeEquivalentTo(serviceResult.Message);
        }
    }
}