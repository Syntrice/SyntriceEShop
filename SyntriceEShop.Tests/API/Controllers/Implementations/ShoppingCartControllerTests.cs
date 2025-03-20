using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.Controllers.Implementations;
using SyntriceEShop.API.Models.ShoppingCartModel.DTO;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Interfaces;
using SyntriceEShop.Tests.API.Services.UserServices;

namespace SyntriceEShop.Tests.API.Controllers.Implementations;

[TestFixture]
public class ShoppingCartControllerTests
{
    private IShoppingCartService _shoppingCartService;
    private ShoppingCartController _controller;
    
    [SetUp]
    public void Setup()
    {
        _shoppingCartService = Substitute.For<IShoppingCartService>();
        _controller = new ShoppingCartController(_shoppingCartService);
    }

    [TestFixture]
    public class AddShoppingCartAsync : ShoppingCartControllerTests
    {
        [Test]
        public async Task CallsShoppingCartService_AddShoppingCartAsync()
        {
            // Arrange
            var addShoppingCartRequest = new AddShoppingCartRequest();
            var addShoppingCartResponse = new ServiceObjectResponse<int>();
            
            _shoppingCartService.AddShoppingCartAsync(addShoppingCartRequest).Returns(addShoppingCartResponse);
            
            // Act
            var result = await _controller.AddShoppingCartAsync(addShoppingCartRequest);
            
            // Assert
            await _shoppingCartService.Received(1).AddShoppingCartAsync(addShoppingCartRequest);
        }

        [Test]
        public async Task WhenShoppingCartService_ReturnsSuccess_ReturnCreatedResult()
        {
            // Arrange
            int id = 1;
            var addShoppingCartRequest = new AddShoppingCartRequest();
            var addShoppingCartResposne = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.Success,
                Value = id
            };
            _shoppingCartService.AddShoppingCartAsync(addShoppingCartRequest).Returns(addShoppingCartResposne);
            
            // Act
            var result = await _controller.AddShoppingCartAsync(addShoppingCartRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(CreatedResult));
        }
        
        [Test]
        public async Task WhenShoppingCartService_ReturnsConflict_ReturnConflictObjectResultWithMessage()
        {
            // Arrange
            int id = 1;
            var addShoppingCartRequest = new AddShoppingCartRequest();
            var addShoppingCartResposne = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.Conflict,
                Value = id,
                Message = "conflict result message"
            };
            _shoppingCartService.AddShoppingCartAsync(addShoppingCartRequest).Returns(addShoppingCartResposne);
            
            // Act
            var result = await _controller.AddShoppingCartAsync(addShoppingCartRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(ConflictObjectResult));
            var resultValue = ((ConflictObjectResult)result).Value as string;
            resultValue.ShouldBe(addShoppingCartResposne.Message);
        }
        
        [Test]
        public async Task WhenShoppingCartService_ReturnsValidationError_ReturnBadRequestObjectResponse()
        {
            // Arrange
            int id = 1;
            var addShoppingCartRequest = new AddShoppingCartRequest();
            var addShoppingCartResposne = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.ValidationError,
                Value = id,
                Message = "message"
            };
            _shoppingCartService.AddShoppingCartAsync(addShoppingCartRequest).Returns(addShoppingCartResposne);
            
            // Act
            var result = await _controller.AddShoppingCartAsync(addShoppingCartRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(BadRequestObjectResult));
            var resultValue = ((BadRequestObjectResult)result).Value as string;
            resultValue.ShouldBe(addShoppingCartResposne.Message);
        }
    }

    [TestFixture]
    public class DeleteShoppingCartAsync : ShoppingCartControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task CallsShoppingCartService_DeleteShoppingCartByIdAsync(int id)
        {
            // Arrange
            var deleteShoppingCartResponse = new ServiceObjectResponse<int>();
            
            _shoppingCartService.DeleteShoppingCartAsync(id).Returns(deleteShoppingCartResponse);
            
            // Act
            var result = await _controller.DeleteShoppingCartByIdAsync(id);
            
            // Assert
            await _shoppingCartService.Received(1).DeleteShoppingCartAsync(id);
        }
        
        [Test]
        public async Task WhenShoppingCartService_ReturnsSuccess_ReturnNoContentResult()
        {
            // Arrange
            int id = 1;
            var deleteShoppingCartResponse = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.Success,
                Value = id
            };
            _shoppingCartService.DeleteShoppingCartAsync(id).Returns(deleteShoppingCartResponse);
            
            // Act
            var result = await _controller.DeleteShoppingCartByIdAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(NoContentResult));
        }
        
        
        [Test]
        public async Task WhenShoppingCartService_ReturnsNotFound_ReturnNotFoundResult()
        {
            // Arrange
            int id = 1;
            var deleteShoppingCartResponse = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.NotFound,
                Value = id
            };
            _shoppingCartService.DeleteShoppingCartAsync(id).Returns(deleteShoppingCartResponse);
            
            // Act
            var result = await _controller.DeleteShoppingCartByIdAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(NotFoundResult));
        }
    }

    [TestFixture]
    public class UpdateShoppingCartByIdAsync : ShoppingCartControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task CallsShoppingCartService_UpdateShoppingCartByIdAsync(int id)
        {
            // Arrange
            var updateShoppingCartRequest = new UpdateShoppingCartRequest();
            var updateShoppingCartResponse = new ServiceResponse();
            
            _shoppingCartService.UpdateShoppingCartByIdAsync(id, updateShoppingCartRequest).Returns(updateShoppingCartResponse);
            
            // Act
            var result = await _controller.UpdateShoppingCartByIdAsync(id, updateShoppingCartRequest);
            
            // Assert
            await _shoppingCartService.Received(1).UpdateShoppingCartByIdAsync(id, updateShoppingCartRequest);
        }
        
        [Test]
        public async Task WhenShoppingCartService_ReturnsSuccess_ReturnNoContentResult()
        {
            // Arrange
            int id = 1;
            var updateShoppingCartRequest = new UpdateShoppingCartRequest();
            var updateShoppingCartResponse = new ServiceResponse()
            {
                Type = ServiceResponseType.Success
            };
            _shoppingCartService.UpdateShoppingCartByIdAsync(id, updateShoppingCartRequest).Returns(updateShoppingCartResponse);
            
            // Act
            var result = await _controller.UpdateShoppingCartByIdAsync(id, updateShoppingCartRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(NoContentResult));
        }

        [Test]
        public async Task WhenShoppingCartService_ReturnsValidationError_ReturnBadRequestObjectResponseWithMessage()
        {
            // Arrange
            int id = 1;
            var updateShoppingCartRequest = new UpdateShoppingCartRequest();
            var updateShoppingCartResponse = new ServiceResponse()
            {
                Type = ServiceResponseType.ValidationError,
                Message = "message"
            };
            _shoppingCartService.UpdateShoppingCartByIdAsync(id, updateShoppingCartRequest)
                .Returns(updateShoppingCartResponse);

            // Act
            var result = await _controller.UpdateShoppingCartByIdAsync(id, updateShoppingCartRequest);

            // Assert
            result.ShouldBeOfType(typeof(BadRequestObjectResult));
            var resultValue = ((BadRequestObjectResult)result).Value as string;
            resultValue.ShouldBe(updateShoppingCartResponse.Message);
        }
        
        [Test]
        public async Task WhenShoppingCartService_ReturnsNotFound_ReturnNotFoundResult()
        {
            // Arrange
            int id = 1;
            var updateShoppingCartRequest = new UpdateShoppingCartRequest();
            var updateShoppingCartResponse = new ServiceResponse()
            {
                Type = ServiceResponseType.NotFound
            };
            _shoppingCartService.UpdateShoppingCartByIdAsync(id, updateShoppingCartRequest).Returns(updateShoppingCartResponse);
            
            // Act
            var result = await _controller.UpdateShoppingCartByIdAsync(id, updateShoppingCartRequest);
            
            // Assert
            result.ShouldBeOfType(typeof(NotFoundResult));
        }
    }
}