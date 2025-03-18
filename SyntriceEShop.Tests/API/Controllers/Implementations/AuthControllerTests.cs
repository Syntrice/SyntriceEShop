using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.ApplicationOptions;
using SyntriceEShop.API.Controllers.Implementations;
using SyntriceEShop.API.Models.AuthModel.DTO;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.Tests.API.Controllers.Implementations;

[TestFixture]
public class AuthControllerTests
{
    private IAuthService _authService;
    private AuthController _authController;
    private IOptions<JWTOptions> _options;
    
    [SetUp]
    public void Setup()
    {
        _authService = Substitute.For<IAuthService>();
        _options = Substitute.For<IOptions<JWTOptions>>();
        var jwtOptions = GetDefaultJWTOptions();
        _options.Value.Returns(jwtOptions);
        _authController = new AuthController(_authService, _options);
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
    public class RegisterAsync : AuthControllerTests
    {
        [Test]
        public async Task CallsAuthService_RegisterAsync_WithUserRegisterDTO()
        {
            // Arrange
            var userRegisterDTO = new AuthRegisterRequest() { Username = "username", Password = "password" };
            _authService.RegisterAsync(userRegisterDTO).Returns(new ServiceResponse());
        
            // Act
            await _authController.RegisterAsync(userRegisterDTO);
        
            // Assert
            await _authService.Received(1).RegisterAsync(userRegisterDTO);
        }
        
        [Test]
        public async Task WhenAuthService_RegisterAsyncReturnsSuccess_ReturnOkResult()
        {
            // Arrange
            var userRegisterDTO = new AuthRegisterRequest() { Username = "username", Password = "password" };
            _authService.RegisterAsync(userRegisterDTO).Returns(new ServiceResponse() {Type = ServiceResponseType.Success});
        
            // Act
            var response = await _authController.RegisterAsync(userRegisterDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(OkResult));
        }
        
        [Test]
        public async Task WhenAuthService_RegisterAsyncReturnsConflict_ReturnsConflictObjectResult()
        {
            // Arrange
            var userRegisterDTO = new AuthRegisterRequest() { Username = "username", Password = "password" };
            _authService.RegisterAsync(userRegisterDTO).Returns(new ServiceResponse() {Type = ServiceResponseType.Conflict});
        
            // Act
            var response = await _authController.RegisterAsync(userRegisterDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(ConflictObjectResult));
        }
    }

    [TestFixture]
    public class LoginAsync : AuthControllerTests
    {
        [Test]
        public async Task CallsAuthService_LoginAsync_WithUseLoginDTO()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            _authService.LoginAsync(userLoginDTO).Returns(new ServiceObjectResponse<AuthLoginResponse>());
        
            // Act
            await _authController.LoginAsync(userLoginDTO);
        
            // Assert
            await _authService.Received(1).LoginAsync(userLoginDTO);
        }
        
        [Test]
        public async Task WhenAuthService_LoginAsyncReturnsSuccess_ReturnOkObjectResultWithTokens()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            var userLoginResponseDTO = new AuthLoginResponse() { AccessToken = "token", RefreshToken = "refreshToken" };
            var serviceResponse = new ServiceObjectResponse<AuthLoginResponse>() {Type = ServiceResponseType.Success, Value = userLoginResponseDTO};
            _authService.LoginAsync(userLoginDTO).Returns(serviceResponse);
        
            // Act
            var response = await _authController.LoginAsync(userLoginDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(OkObjectResult));
            var responseValue = (response as OkObjectResult)?.Value as AuthLoginResponse;
            responseValue.ShouldBe(userLoginResponseDTO);
        }
        
        [Test]
        public async Task WhenAuthService_LoginAsyncReturnsNotFound_ReturnNotFoundObjectResult()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            var serviceResponse = new ServiceObjectResponse<AuthLoginResponse>() {Type = ServiceResponseType.NotFound};
            _authService.LoginAsync(userLoginDTO).Returns(serviceResponse);
        
            // Act
            var response = await _authController.LoginAsync(userLoginDTO);
        
            // Assert
            response.ShouldBeOfType(typeof(NotFoundObjectResult));
        }
        
        [Test]
        public async Task WhenAuthService_LoginAsyncReturnsInvalidCredentials_ReturnUnauthorizedObjectResult()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            var serviceResponse = new ServiceObjectResponse<AuthLoginResponse>() {Type = ServiceResponseType.InvalidCredentials};
            _authService.LoginAsync(userLoginDTO).Returns(serviceResponse);
        
            // Act
            var response = await _authController.LoginAsync(userLoginDTO);
        
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
    public class RefreshAsync : AuthControllerTests
    {
        [Test]
        public async Task CallsAuthService_RefreshAsync_WithUserRefreshRequestDTO()
        {
            // Arrange
            var userRefreshRequestDto = new AuthRefreshRequest() { RefreshToken = "refreshToken" };
            var response = new ServiceObjectResponse<AuthRefreshResponse>();
            _authService.RefreshAsync(userRefreshRequestDto).Returns(response);

            // Act
            await _authController.RefreshAsync(userRefreshRequestDto);

            // Assert
            await _authService.Received(1).RefreshAsync(userRefreshRequestDto);
        }

        [Test]
        public async Task WhenAuthService_RefreshAsyncReturnsInvalidCredentials_ReturnUnauthorizedObjectResult()
        {
            // Arrange
            var userRefreshRequestDto = new AuthRefreshRequest() { RefreshToken = "refreshToken" };
            var response = new ServiceObjectResponse<AuthRefreshResponse>()
                { Type = ServiceResponseType.InvalidCredentials };
            _authService.RefreshAsync(userRefreshRequestDto).Returns(response);

            // Act
            var result = await _authController.RefreshAsync(userRefreshRequestDto);

            // Assert
            result.ShouldBeOfType(typeof(UnauthorizedObjectResult));
        }

        [Test]
        public async Task WhenAuthService_RefreshAsyncReturnsSuccess_ReturnOkObjectResultWithTokens()
        {
            // Arrange
            var refreshRequestDTO = new AuthRefreshRequest() { RefreshToken = "refreshToken" };
            var userRefreshResponseDTO = new AuthRefreshResponse() { AccessToken = "token", RefreshToken = "refreshToken" };
            var response = new ServiceObjectResponse<AuthRefreshResponse>()
                { Type = ServiceResponseType.Success, Value = userRefreshResponseDTO};
            _authService.RefreshAsync(refreshRequestDTO).Returns(response);

            // Act
            var result = await _authController.RefreshAsync(refreshRequestDTO);

            // Assert
            result.ShouldBeOfType(typeof(OkObjectResult));
            var resultValue = (result as OkObjectResult)?.Value as AuthRefreshResponse;
            resultValue.ShouldBe(userRefreshResponseDTO);
        }
    }
}