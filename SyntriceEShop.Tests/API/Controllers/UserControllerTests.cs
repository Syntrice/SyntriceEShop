using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.Controllers;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.UserServices;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.Tests.API.Controllers;

[TestFixture]
public class UserControllerTests
{
    private IUserService _userService;
    private UserController _userController;
    
    [SetUp]
    public void Setup()
    {
        _userService = Substitute.For<IUserService>();
        _userController = new UserController(_userService);
    }

    [TestFixture]
    public class RegisterAsync : UserControllerTests
    {
        [Test]
        public async Task CallsUserService_RegisterAsync_WithUserRegisterDTO()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
            _userService.RegisterAsync(userRegisterDTO).Returns(new ServiceResponse());
        
            // Act
            await _userController.RegisterAsync(userRegisterDTO);
        
            // Assert
            await _userService.Received(1).RegisterAsync(userRegisterDTO);
        }
        
        [Test]
        public async Task WhenUserService_RegisterAsyncReturnsSuccess_ReturnOkResult()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
            _userService.RegisterAsync(userRegisterDTO).Returns(new ServiceResponse() {Type = ServiceResponseType.Success});
        
            // Act
            var response = await _userController.RegisterAsync(userRegisterDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(OkResult));
        }
        
        [Test]
        public async Task WhenUserService_RegisterAsyncReturnsConflict_ReturnsConflictObjectResult()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
            _userService.RegisterAsync(userRegisterDTO).Returns(new ServiceResponse() {Type = ServiceResponseType.Conflict});
        
            // Act
            var response = await _userController.RegisterAsync(userRegisterDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(ConflictObjectResult));
        }
    }

    [TestFixture]
    public class LoginAsync : UserControllerTests
    {
        [Test]
        public async Task CallsUserService_LoginAsync_WithUseLoginDTO()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO() { Username = "username", Password = "password" };
            _userService.LoginAsync(userLoginDTO).Returns(new ServiceObjectResponse<string>());
        
            // Act
            await _userController.LoginAsync(userLoginDTO);
        
            // Assert
            await _userService.Received(1).LoginAsync(userLoginDTO);
        }
        
        [Test]
        public async Task WhenUserService_LoginAsyncReturnsSuccess_ReturnOkObjectResultWithToken()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO() { Username = "username", Password = "password" };
            var serviceResponse = new ServiceObjectResponse<string>() {Type = ServiceResponseType.Success, Value = "token"};
            _userService.LoginAsync(userLoginDTO).Returns(serviceResponse);
        
            // Act
            var response = await _userController.LoginAsync(userLoginDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (response as OkObjectResult)?.Value as string;
            responseValue.ShouldBe("token");
        }
        
        [Test]
        public async Task WhenUserService_LoginAsyncReturnsNotFound_ReturnNotFoundObjectResult()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO() { Username = "username", Password = "password" };
            var serviceResponse = new ServiceObjectResponse<string>() {Type = ServiceResponseType.NotFound};
            _userService.LoginAsync(userLoginDTO).Returns(serviceResponse);
        
            // Act
            var response = await _userController.LoginAsync(userLoginDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(NotFoundObjectResult));
        }
        
        [Test]
        public async Task WhenUserService_LoginAsyncReturnsInvalidCredentials_ReturnUnauthorizedObjectResult()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO() { Username = "username", Password = "password" };
            var serviceResponse = new ServiceObjectResponse<string>() {Type = ServiceResponseType.InvalidCredentials};
            _userService.LoginAsync(userLoginDTO).Returns(serviceResponse);
        
            // Act
            var response = await _userController.LoginAsync(userLoginDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(UnauthorizedObjectResult));
        }
    }

    
}