using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.Controllers.Implementations;
using SyntriceEShop.API.Models.OrderModel.DTO;
using SyntriceEShop.API.Models.ProductModel.DTO;
using SyntriceEShop.API.Models.ShoppingCartModel.DTO;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.Tests.API.Controllers.Implementations;

[TestFixture]
public class UserControllerTests
{
    private IUserService _userService;
    private UserController _controller;
    
    [SetUp]
    public void Setup()
    {
        _userService = Substitute.For<IUserService>();
        _controller = new UserController(_userService);
    }

    [TestFixture]
    public class GetAllProductsByUserAsync : UserControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task CallsUserService_GetAllProductsByUserAsync(int id)
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>();
            _userService.GetAllProductsByUserAsync(id).Returns(serviceResult);
            
            // Act
            await _controller.GetAllProductsByUserAsync(id);
            
            // Assert
            await _userService.Received(1).GetAllProductsByUserAsync(id);
        }

        [Test]
        public async Task WhenUserService_ReturnsSuccess_ReturnOkObjectResultWithValue()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>()
            {
                Type = ServiceResponseType.Success,
                Value = [new GetProductResponse(), new GetProductResponse()]
            };
            _userService.GetAllProductsByUserAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetAllProductsByUserAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as IEnumerable<GetProductResponse>;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
        
        [Test]
        public async Task WhenUserService_ReturnsEmptyList_ReturnOkObjectResultWithEmptyList()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>()
            {
                Type = ServiceResponseType.Success,
                Value = []
            };
            _userService.GetAllProductsByUserAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetAllProductsByUserAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as IEnumerable<GetProductResponse>;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
        
        [Test]
        public async Task WhenUserService_ReturnsNotFound_ReturnNotFoundResult()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetProductResponse>>()
            {
                Type = ServiceResponseType.NotFound
            };
            _userService.GetAllProductsByUserAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetAllProductsByUserAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(NotFoundResult));
        }
    }

    [TestFixture]
    public class GetAllOrdersByUserAsync : UserControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task CallsUserService_GetAllOrdersByUserAsync(int id)
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetOrderResponse>>();
            _userService.GetAllOrdersByUserAsync(id).Returns(serviceResult);
            
            // Act
            await _controller.GetAllOrdersByUserAsync(id);
            
            // Assert
            await _userService.Received(1).GetAllOrdersByUserAsync(id);
        }

        [Test]
        public async Task WhenUserService_ReturnsSuccess_ReturnOkObjectResultWithValue()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetOrderResponse>>()
            {
                Type = ServiceResponseType.Success,
                Value = [new GetOrderResponse(), new GetOrderResponse()]
            };
            _userService.GetAllOrdersByUserAsync(id).Returns(serviceResult);

            // Act
            var result = await _controller.GetAllOrdersByUserAsync(id);

            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as IEnumerable<GetOrderResponse>;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
        
        [Test]
        public async Task WhenUserService_ReturnsEmptyList_ReturnOkObjectResultWithEmptyList()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetOrderResponse>>()
            {
                Type = ServiceResponseType.Success,
                Value = []
            };
            _userService.GetAllOrdersByUserAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetAllOrdersByUserAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as IEnumerable<GetOrderResponse>;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
        
        [Test]
        public async Task WhenUserService_ReturnsNotFound_ReturnNotFoundResult()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetOrderResponse>>()
            {
                Type = ServiceResponseType.NotFound
            };
            _userService.GetAllOrdersByUserAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetAllOrdersByUserAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(NotFoundResult));
        }
    }

    [TestFixture]
    public class GetShoppingCartByUserAsync : UserControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task CallsUserService_GetShoppingCartByUserAsync(int id)
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<GetShoppingCartResponse>();
            _userService.GetShoppingCartByUserAsync(id).Returns(serviceResult);
            
            // Act
            await _controller.GetShoppingCartByUserAsync(id);
            
            // Assert
            await _userService.Received(1).GetShoppingCartByUserAsync(id);
        }
        
        [Test]
        public async Task WhenUserService_ReturnsSuccess_ReturnOkObjectResultWithValue()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<GetShoppingCartResponse>()
            {
                Type = ServiceResponseType.Success,
                Value = new GetShoppingCartResponse()
            };
            _userService.GetShoppingCartByUserAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetShoppingCartByUserAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as GetShoppingCartResponse;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
        
        [Test]
        public async Task WhenUserService_ReturnsNotFound_ReturnNotFoundResult()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<GetShoppingCartResponse>()
            {
                Type = ServiceResponseType.NotFound
            };
            _userService.GetShoppingCartByUserAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _controller.GetShoppingCartByUserAsync(id);
            
            // Assert
            result.ShouldBeOfType(typeof(NotFoundResult));
        }
    }
}