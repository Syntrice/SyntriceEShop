using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.ApplicationOptions;
using SyntriceEShop.API.Controllers;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.UserServices;
using SyntriceEShop.API.Services.UserServices.Models;

namespace SyntriceEShop.Tests.API.Controllers;

[TestFixture]
public class UserControllerTests
{
    private IUserService _userService;
    private UserController _userController;
    private IOptions<JWTOptions> _options;
    
    [SetUp]
    public void Setup()
    {
        _userService = Substitute.For<IUserService>();
        _options = Substitute.For<IOptions<JWTOptions>>();
        var jwtOptions = GetDefaultJWTOptions();
        _options.Value.Returns(jwtOptions);
        _userController = new UserController(_userService, _options);
    }
    
    public static JWTOptions GetDefaultJWTOptions()
    {
        return new JWTOptions()
        {
            SecretKey = "d6bW9fMV3tCx7FAZpxc5doDpIRbWkxSk", 
            Issuer = "automated", 
            Audience = "test",
            ExpirationInMinutes = 30,
            RefreshTokenSize = 32,
            RefreshTokenExpirationInDays = 7
        };
    }

    [TestFixture]
    public class RegisterAsync : UserControllerTests
    {
        [Test]
        public async Task CallsUserService_RegisterAsync_WithUserRegisterDTO()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterRequestDTO() { Username = "username", Password = "password" };
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
            var userRegisterDTO = new UserRegisterRequestDTO() { Username = "username", Password = "password" };
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
            var userRegisterDTO = new UserRegisterRequestDTO() { Username = "username", Password = "password" };
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
            var userLoginDTO = new UserLoginRequestDTO() { Username = "username", Password = "password" };
            _userService.LoginAsync(userLoginDTO).Returns(new ServiceObjectResponse<UserLoginResponseDTO>());
        
            // Act
            await _userController.LoginAsync(userLoginDTO);
        
            // Assert
            await _userService.Received(1).LoginAsync(userLoginDTO);
        }
        
        [Test]
        public async Task WhenUserService_LoginAsyncReturnsSuccess_ReturnOkObjectResultWithTokens()
        {
            // Arrange
            var userLoginDTO = new UserLoginRequestDTO() { Username = "username", Password = "password" };
            var userLoginResponseDTO = new UserLoginResponseDTO() { AccessToken = "token", RefreshToken = "refreshToken" };
            var serviceResponse = new ServiceObjectResponse<UserLoginResponseDTO>() {Type = ServiceResponseType.Success, Value = userLoginResponseDTO};
            _userService.LoginAsync(userLoginDTO).Returns(serviceResponse);
        
            // Act
            var response = await _userController.LoginAsync(userLoginDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (response as OkObjectResult)?.Value as UserLoginResponseDTO;
            responseValue.ShouldBe(userLoginResponseDTO);
        }
        
        [Test]
        public async Task WhenUserService_LoginAsyncReturnsNotFound_ReturnNotFoundObjectResult()
        {
            // Arrange
            var userLoginDTO = new UserLoginRequestDTO() { Username = "username", Password = "password" };
            var serviceResponse = new ServiceObjectResponse<UserLoginResponseDTO>() {Type = ServiceResponseType.NotFound};
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
            var userLoginDTO = new UserLoginRequestDTO() { Username = "username", Password = "password" };
            var serviceResponse = new ServiceObjectResponse<UserLoginResponseDTO>() {Type = ServiceResponseType.InvalidCredentials};
            _userService.LoginAsync(userLoginDTO).Returns(serviceResponse);
        
            // Act
            var response = await _userController.LoginAsync(userLoginDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(UnauthorizedObjectResult));
        }
        
        [Test]
        public async Task WhenUseCookiesTrue_ReturnsRefreshTokenCookie()
        {
            // TODO
            Assert.Ignore();
        }

        [Test]
        public async Task WhenUseCookiesTrue_ReturnsAccessTokenCookie()
        {
            // TODO
            Assert.Ignore();
        }

        [Test]
        public async Task WhenUseCookiesTrue_ReturnsEmptyBody()
        {
            // TODO
            Assert.Ignore();
        }
    }

    [TestFixture]
    public class RefreshAsync : UserControllerTests
    {
        [Test]
        public async Task CallsUserService_RefreshAsync_WithUserRefreshRequestDTO()
        {
            // Arrange
            var userRefreshRequestDto = new UserRefreshRequestDTO() { RefreshToken = "refreshToken" };
            var response = new ServiceObjectResponse<UserRefreshResponseDTO>();
            _userService.RefreshAsync(userRefreshRequestDto).Returns(response);

            // Act
            await _userController.RefreshAsync(userRefreshRequestDto);

            // Assert
            await _userService.Received(1).RefreshAsync(userRefreshRequestDto);
        }

        [Test]
        public async Task WhenUserService_RefreshAsyncReturnsInvalidCredentials_ReturnUnauthorizedObjectResult()
        {
            // Arrange
            var userRefreshRequestDto = new UserRefreshRequestDTO() { RefreshToken = "refreshToken" };
            var response = new ServiceObjectResponse<UserRefreshResponseDTO>()
                { Type = ServiceResponseType.InvalidCredentials };
            _userService.RefreshAsync(userRefreshRequestDto).Returns(response);

            // Act
            var result = await _userController.RefreshAsync(userRefreshRequestDto);

            // Assert
            result.ShouldBeOfType(typeof(UnauthorizedObjectResult));
        }

        [Test]
        public async Task WhenUserService_RefreshAsyncReturnsSuccess_ReturnOkObjectResultWithTokens()
        {
            // Arrange
            var refreshRequestDTO = new UserRefreshRequestDTO() { RefreshToken = "refreshToken" };
            var userRefreshResponseDTO = new UserRefreshResponseDTO() { AccessToken = "token", RefreshToken = "refreshToken" };
            var response = new ServiceObjectResponse<UserRefreshResponseDTO>()
                { Type = ServiceResponseType.Success, Value = userRefreshResponseDTO};
            _userService.RefreshAsync(refreshRequestDTO).Returns(response);

            // Act
            var result = await _userController.RefreshAsync(refreshRequestDTO);

            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var resultValue = (result as OkObjectResult)?.Value as UserRefreshResponseDTO;
            resultValue.ShouldBe(userRefreshResponseDTO);
        }
    }
}