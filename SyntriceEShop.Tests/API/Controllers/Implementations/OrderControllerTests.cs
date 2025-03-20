using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.Controllers.Implementations;
using SyntriceEShop.API.Models.OrderModel.DTO;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Interfaces;
using SyntriceEShop.Tests.API.Services.UserServices;

namespace SyntriceEShop.Tests.API.Controllers.Implementations;

[TestFixture]
public class OrderControllerTests
{
    private OrderController _orderController;
    private IOrderService _orderService;

    [SetUp]
    public void Setup()
    {
        _orderService = Substitute.For<IOrderService>();
        _orderController = new OrderController(_orderService);
    }

    [TestFixture]
    public class GetAllOrdersAsync : OrderControllerTests
    {
        [Test]
        public async Task CallsOrderService_GetAllOrdersAsync()
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetOrderResponse>>();
            _orderService.GetAllOrdersAsync().Returns(serviceResult);

            // Act
            await _orderController.GetAllOrdersAsync();

            // Assert
            await _orderService.Received(1).GetAllOrdersAsync();
        }

        [Test]
        public async Task WhenOrderService_ReturnsEmptyCollection_ReturnsOkObjectResultWithEmptyCollection()
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetOrderResponse>>()
            {
                Type = ServiceResponseType.Success,
                Value = []
            };
            _orderService.GetAllOrdersAsync().Returns(serviceResult);

            // Act
            var result = await _orderController.GetAllOrdersAsync();

            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as IEnumerable<GetOrderResponse>;
            responseValue.ShouldBeEmpty();
        }

        [Test]
        public async Task WhenOrderService_ReturnsSuccess_ReturnsOkResultWithOrders()
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<IEnumerable<GetOrderResponse>>()
            {
                Type = ServiceResponseType.Success,
                Value = [new GetOrderResponse(), new GetOrderResponse()]
            };
            _orderService.GetAllOrdersAsync().Returns(serviceResult);

            // Act
            var result = await _orderController.GetAllOrdersAsync();

            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as IEnumerable<GetOrderResponse>;
            responseValue.ShouldBeEquivalentTo(serviceResult.Value);
        }
    }

    [TestFixture]
    public class GetOrderByIdAsync : OrderControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(98)]
        public async Task CallsOrderService_GetOrderByIdAsync(int id)
        {
            // Arrange
            var serviceResult = new ServiceObjectResponse<GetOrderResponse>()
            {
                Type = ServiceResponseType.Failure,
            };
            _orderService.GetOrderByIdAsync(id).Returns(serviceResult);


            // Act
            var result = await _orderController.GetOrderByIdAsync(id);

            // Assert
            await _orderService.Received(1).GetOrderByIdAsync(id);
        }

        [Test]
        public async Task WhenOrderService_ReturnsSuccess_ReturnsOkObjectResultWithOrder()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<GetOrderResponse>()
            {
                Type = ServiceResponseType.Success,
                Value = new GetOrderResponse()
            };
            _orderService.GetOrderByIdAsync(id).Returns(serviceResult);


            // Act
            var result = await _orderController.GetOrderByIdAsync(id);

            // Assert

            result.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (result as OkObjectResult)?.Value as GetOrderResponse;
            responseValue.ShouldBe(serviceResult.Value);
        }

        [Test]
        public async Task WhenOrderService_ReturnsNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceObjectResponse<GetOrderResponse>()
            {
                Type = ServiceResponseType.NotFound,
            };
            _orderService.GetOrderByIdAsync(id).Returns(serviceResult);


            // Act
            var result = await _orderController.GetOrderByIdAsync(id);

            // Assert

            result.ShouldBeOfType(typeof(NotFoundResult));
        }
    }


    [TestFixture]
    public class AddOrderAsync : OrderControllerTests
    {
        [Test]
        public async Task CallsOrderService_AddOrderAsync()
        {
            // Arrange
            var addOrderRequest = new AddOrderRequest();
            var serviceResult = new ServiceObjectResponse<int>();
            _orderService.AddOrderAsync(addOrderRequest).Returns(serviceResult);
            
            // Act
            await _orderController.AddOrderAsync(addOrderRequest);
            
            // Assert
            await _orderService.Received(1).AddOrderAsync(addOrderRequest);
        }

        [TestCase]
        public async Task WhenOrderService_ReturnsSuccess_ReturnCreatedAtRouteResult()
        {
            // Arrange
            int id = 1;
            var addOrderRequest = new AddOrderRequest();
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.Success,
                Value = id
            };
            _orderService.AddOrderAsync(addOrderRequest).Returns(serviceResult);
            
            // Act
            var result = await _orderController.AddOrderAsync(addOrderRequest);
            
            // Assert
            result.ShouldBeOfType<CreatedAtRouteResult>();
        }

        [Test]
        public async Task WhenOrderService_ReturnsConflict_ReturnsConflictObjectResultWithMessage()
        {
            // Arrange
            var addOrderRequest = new AddOrderRequest();
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.Conflict,
                Message = "message"
            };
            _orderService.AddOrderAsync(addOrderRequest).Returns(serviceResult);
            
            // Act
            var result = await _orderController.AddOrderAsync(addOrderRequest);
            
            // Assert
            result.ShouldBeOfType<ConflictObjectResult>();
            var resutlValue = ((ConflictObjectResult) result).Value as string;
            resutlValue.ShouldBe(serviceResult.Message);
        }

        [Test]
        public async Task WhenOrderService_ReturnsValidationError_ReturnsBadRequestResultWithErrors()
        {
            // Arrange
            var addOrderRequest = new AddOrderRequest();
            var serviceResult = new ServiceObjectResponse<int>()
            {
                Type = ServiceResponseType.ValidationError,
                Message = "message"
            };
            _orderService.AddOrderAsync(addOrderRequest).Returns(serviceResult);
            
            // Act
            var result = await _orderController.AddOrderAsync(addOrderRequest);
            
            // Assert
            result.ShouldBeOfType<BadRequestObjectResult>();
            var resutlValue = ((BadRequestObjectResult) result).Value as string;
            resutlValue.ShouldBe(serviceResult.Message);
        }
    }

    [TestFixture]
    public class DeleteOrderByIdAsync : OrderControllerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(99)]
        public async Task CallsOrderService_DeleteOrderByIdAsync(int id)
        {
            // Arrange
            var serviceResult = new ServiceResponse();
            _orderService.DeleteOrderByIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _orderController.DeleteOrderByIdAsync(id);
            
            // Assert
            await _orderService.Received(1).DeleteOrderByIdAsync(id);
        }
        
        [Test]
        public async Task WhenOrderService_ReturnsSuccess_ReturnsNoContentResult()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceResponse()
            {
                Type = ServiceResponseType.Success,
            };
            _orderService.DeleteOrderByIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _orderController.DeleteOrderByIdAsync(id);
            
            // Assert
            result.ShouldBeOfType<NoContentResult>();
        }
        
        [Test]
        public async Task WhenOrderService_ReturnsNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            int id = 1;
            var serviceResult = new ServiceResponse()
            {
                Type = ServiceResponseType.NotFound,
            };
            _orderService.DeleteOrderByIdAsync(id).Returns(serviceResult);
            
            // Act
            var result = await _orderController.DeleteOrderByIdAsync(id);
            
            // Assert
            result.ShouldBeOfType<NotFoundResult>();
        }
    }
}