using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using SyntriceEShop.API.Models.AuthModel.DTO;
using SyntriceEShop.API.Models.RefreshTokenModel;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Repositories;
using SyntriceEShop.API.Repositories.Interfaces;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Implementations;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.Tests.API.Services.UserServices;

[TestFixture]
public class AuthServiceTests
{
    private IUserRepository _repository;
    private IUnitOfWork _unitOfWork;
    private IPasswordHasher _passwordHasher;
    private IJWTProvider _tokenProvider;
    private AuthService _authService;
    private IRefreshTokenRepository _refreshTokenRepository;

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<IUserRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _tokenProvider = Substitute.For<IJWTProvider>();
        _refreshTokenRepository = Substitute.For<IRefreshTokenRepository>();
        _authService = new AuthService(_repository, _refreshTokenRepository, _unitOfWork, _passwordHasher, _tokenProvider);
    }
    
    [TestFixture]
    public class RegisterAsync : AuthServiceTests
    {
        [Test]
        public async Task CallsUnitOfWork_SaveChangesAsync()
        {
            // Arrange
            var userRegisterDTO = new AuthRegisterRequest() { Username = "username", Password = "password" };

            // Act
            await _authService.RegisterAsync(userRegisterDTO);

            // Assert
            await _unitOfWork.Received(1).SaveChangesAsync();
        }

        [Test]
        public async Task CallsRepository_Add()
        {
            // Arrange
            var userRegisterDTO = new AuthRegisterRequest() { Username = "username", Password = "password" };

            // Act
            await _authService.RegisterAsync(userRegisterDTO);

            // Assert
            _repository.Received(1).Add(Arg.Any<User>());
        }

        [Test]
        public async Task CallsPasswordHasher_Hash_WithRegisterPassword()
        {
            // Arrange
            var userRegisterDTO = new AuthRegisterRequest() { Username = "username", Password = "password" };

            // Act
            await _authService.RegisterAsync(userRegisterDTO);

            // Assert
            _passwordHasher.Received(1).Hash(userRegisterDTO.Password);
        }

        [Test]
        public async Task CallsRepository_UsernameExistsAsync()
        {
            // Arrange
            var userRegisterDTO = new AuthRegisterRequest() { Username = "username", Password = "password" };

            // Act
            await _authService.RegisterAsync(userRegisterDTO);

            // Assert
            await _repository.Received(1).UsernameExistsAsync(userRegisterDTO.Username);
        }

        [Test]
        public async Task WhenRepositoryUsernameExists_ReturnsConflictResponseType()
        {
            // Arrange
            var userRegisterDTO = new AuthRegisterRequest() { Username = "username", Password = "password" };
            _repository.UsernameExistsAsync(userRegisterDTO.Username).Returns(true);

            // Act
            var response = await _authService.RegisterAsync(userRegisterDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.Conflict);
        }

        [Test]
        public async Task WhenRepositoryUsernameDoesNotExist_ReturnsSuccessResponseType()
        {
            // Arrange
            var userRegisterDTO = new AuthRegisterRequest() { Username = "username", Password = "password" };
            _repository.UsernameExistsAsync(userRegisterDTO.Username).Returns(false);

            // Act
            var response = await _authService.RegisterAsync(userRegisterDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.Success);
        }
    }

    [TestFixture]
    public class LoginAsync : AuthServiceTests
    {
        [Test]
        public async Task CallsRepository_GetUserByUsernameAsync_WithCorrectParameters()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };

            // Act
            await _authService.LoginAsync(userLoginDTO);

            // Assert
            await _repository.Received(1).GetUserByUsernameAsync(userLoginDTO.Username);
        }
        
        [Test]
        public async Task CallsPasswordHasher_Verify_WithCorrectParameters()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            var userEntity = new User() { Username = "username", PasswordHash = "hashedpassword" };
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).Returns(userEntity);

            // Act
            await _authService.LoginAsync(userLoginDTO);

            // Assert
            _passwordHasher.Received(1).Verify(userLoginDTO.Password, userEntity.PasswordHash);
        }

        [Test]
        public async Task CallsTokenProvider_GenerateToken_WithUserEntity()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            var userEntity = new User() { Username = "username", PasswordHash = "hashedpassword" };
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).Returns(userEntity);
            _passwordHasher.Verify(userLoginDTO.Password, userEntity.PasswordHash).Returns(true);
            _tokenProvider.GenerateToken(userEntity).Returns("token");
            _tokenProvider.GenerateRefreshToken(userEntity).Returns(new RefreshToken());

            // Act
            await _authService.LoginAsync(userLoginDTO);

            // Assert
            _tokenProvider.Received(1).GenerateToken(userEntity);
        }
        
        [Test]
        public async Task CallsTokenProvider_GenerateRefreshToken_WithUserEntity()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            var userEntity = new User() { Username = "username", PasswordHash = "hashedpassword" };
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).Returns(userEntity);
            _passwordHasher.Verify(userLoginDTO.Password, userEntity.PasswordHash).Returns(true);
            _tokenProvider.GenerateToken(userEntity).Returns("token");
            _tokenProvider.GenerateRefreshToken(userEntity).Returns(new RefreshToken());
            
            // Act
            await _authService.LoginAsync(userLoginDTO);

            // Assert
            _tokenProvider.Received(1).GenerateRefreshToken(userEntity);

        }

        [Test]
        public async Task WhenRepository_GetByUsernameReturnsNull_ReturnsNotFoundResponseType()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).ReturnsNull();

            // Act
            var response = await _authService.LoginAsync(userLoginDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.NotFound);
        }
        
        [Test]
        public async Task WhenPasswordHasher_VerifyReturnsFalse_ReturnsInvalidCredentialsResponseType()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).ReturnsNull();

            // Act
            var response = await _authService.LoginAsync(userLoginDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.NotFound);
        }

        [Test]
        public async Task WhenAllChecksPass_ReturnsSuccessResponseTypeWithJwtToken()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            var userEntity = new User() { Username = "username", PasswordHash = "hashedpassword" };
            var jwtToken = "jwtToken";
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).Returns(userEntity);
            _passwordHasher.Verify(userLoginDTO.Password, userEntity.PasswordHash).Returns(true);
            _tokenProvider.GenerateToken(userEntity).Returns(jwtToken);
            _tokenProvider.GenerateRefreshToken(userEntity).Returns(new RefreshToken());

            // Act
            var response = await _authService.LoginAsync(userLoginDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.Success);
            response.Value.AccessToken.ShouldBe(jwtToken);
        }
        
        [Test]
        public async Task WhenAllChecksPass_ReturnsSuccessResponseTypeWithRefreshToken()
        {
            // Arrange
            var userLoginDTO = new AuthLoginRequest() { Username = "username", Password = "password" };
            var userEntity = new User() { Id = 1, Username = "username", PasswordHash = "hashedpassword" };
            var refreshToken = new RefreshToken()
            {
                Id = Guid.Empty,
                Token = "refreshToken",
                UserId = 1,
                ExpiresOnUTC = DateTime.UtcNow,
            };
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).Returns(userEntity);
            _passwordHasher.Verify(userLoginDTO.Password, userEntity.PasswordHash).Returns(true);
            _tokenProvider.GenerateRefreshToken(userEntity).Returns(refreshToken);

            // Act
            var response = await _authService.LoginAsync(userLoginDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.Success);
            response.Value.RefreshToken.ShouldBe(refreshToken.Token);
        }
    }

    [TestFixture]
    public class RefreshAsync : AuthServiceTests
    {
        [Test]
        public async Task CallsRefreshTokenRepository_GetByTokenValue_WithInputTokenValue()
        {
            // Arrange
            var userRefreshRequestDto = new AuthRefreshRequest() { RefreshToken = "refreshToken" };

            // Act
            var result = await _authService.RefreshAsync(userRefreshRequestDto);

            // Assert
            await _refreshTokenRepository.Received(1).GetByTokenValue(userRefreshRequestDto.RefreshToken);
        }

        [Test]
        public async Task CallsTokenProvider_GenerateToken_WithRefreshTokenUser()
        {
            // Arrange
            var userRefreshRequestDto = new AuthRefreshRequest() { RefreshToken = "refreshToken" };
            var user = new User() { Id = 1, Username = "username", PasswordHash = "hashedpassword" };
            var refreshToken = new RefreshToken() { Id = Guid.NewGuid(), UserId = user.Id, Token = userRefreshRequestDto.RefreshToken, ExpiresOnUTC = DateTime.UtcNow.AddDays(7), User = user};
            _refreshTokenRepository.GetByTokenValue(userRefreshRequestDto.RefreshToken).Returns(refreshToken);
            _tokenProvider.UpdateRefreshToken(refreshToken).Returns(refreshToken);
            
            // Act
            var result = await _authService.RefreshAsync(userRefreshRequestDto);
            
            // Assert
            _tokenProvider.Received(1).GenerateToken(refreshToken.User);
        }
        
        [Test]
        public async Task CallsTokenProvider_UpdateRefreshToken_WithInputRefreshToken()
        {
            // Arrange
            var userRefreshRequestDto = new AuthRefreshRequest() { RefreshToken = "refreshToken" };
            var user = new User() { Id = 1, Username = "username", PasswordHash = "hashedpassword" };
            var refreshToken = new RefreshToken() { Id = Guid.NewGuid(), UserId = user.Id, Token = userRefreshRequestDto.RefreshToken, ExpiresOnUTC = DateTime.UtcNow.AddDays(7), User = user};
            _refreshTokenRepository.GetByTokenValue(userRefreshRequestDto.RefreshToken).Returns(refreshToken);
            _tokenProvider.UpdateRefreshToken(refreshToken).Returns(refreshToken);
            
            // Act
            var result = await _authService.RefreshAsync(userRefreshRequestDto);
            
            // Assert
            _tokenProvider.Received(1).UpdateRefreshToken(refreshToken);
        }
        
        [Test]
        public async Task WithNullRefreshToken_ReturnsInvalidCredentialsResponseType()
        {
            // Arrange
            var userRefreshRequestDto = new AuthRefreshRequest() { RefreshToken = "refreshToken" };
            _refreshTokenRepository.GetByTokenValue(userRefreshRequestDto.RefreshToken).ReturnsNull();

            
            // Act
            var result = await _authService.RefreshAsync(userRefreshRequestDto);
            
            // Assert
            result.Type.ShouldBe(ServiceResponseType.InvalidCredentials);
        }
        
        [Test]
        public async Task WithExpiredRefreshToken_ReturnsInvalidCredentialsResponseType()
        {
            // Arrange
            var userRefreshRequestDto = new AuthRefreshRequest() { RefreshToken = "refreshToken" };
            var user = new User() { Id = 1, Username = "username", PasswordHash = "hashedpassword" };
            var refreshToken = new RefreshToken() { Id = Guid.NewGuid(), UserId = user.Id, Token = userRefreshRequestDto.RefreshToken, ExpiresOnUTC = DateTime.UtcNow.Subtract(TimeSpan.FromDays(7)), User = user};
            _refreshTokenRepository.GetByTokenValue(userRefreshRequestDto.RefreshToken).Returns(refreshToken);
            
            // Act
            var result = await _authService.RefreshAsync(userRefreshRequestDto);
            
            // Assert
            result.Type.ShouldBe(ServiceResponseType.InvalidCredentials);
        }
        
        [Test]
        public async Task WithValidToken_ReturnsSuccessResponseTypeWithResponseDT()
        {
            // Arrange
            var userRefreshRequestDto = new AuthRefreshRequest() { RefreshToken = "refreshToken" };
            var user = new User() { Id = 1, Username = "username", PasswordHash = "hashedpassword" };
            
            var refreshToken = new RefreshToken() { Id = Guid.NewGuid(), UserId = user.Id, Token = userRefreshRequestDto.RefreshToken, ExpiresOnUTC = DateTime.UtcNow.AddDays(7), User = user};
            var updatedRefreshToken = new RefreshToken() { Id = refreshToken.Id, UserId = user.Id, Token = "updatedRefreshToken", ExpiresOnUTC = DateTime.UtcNow.AddDays(8), User = user};
            var updatedAccessToken = "updatedAccessToken";
            
            var response = new AuthRefreshResponse() { RefreshToken = updatedRefreshToken.Token, AccessToken = updatedAccessToken };
            _refreshTokenRepository.GetByTokenValue(userRefreshRequestDto.RefreshToken).Returns(refreshToken);
            _tokenProvider.GenerateToken(user).Returns(updatedAccessToken);
            _tokenProvider.UpdateRefreshToken(refreshToken).Returns(updatedRefreshToken);
            
            
            // Act
            var result = await _authService.RefreshAsync(userRefreshRequestDto);
            
            // Assert
            result.Type.ShouldBe(ServiceResponseType.Success);
            result.Value.RefreshToken.ShouldBe(updatedRefreshToken.Token);
            result.Value.AccessToken.ShouldBe(updatedAccessToken);
        }
    }

    [TestFixture]
    public class RevokeRefreshTokensAsync : AuthServiceTests
    {
        [Test]
        public async Task CallsRefreshTokenRepository_RemoveAllByUserIdAsync_WithInputUserId()
        {
            // Arrange
            var userId = 1;
            
            // Act
            var result = await _authService.RevokeRefreshTokensAsync(userId);
            
            // Assert
            await _refreshTokenRepository.Received(1).RemoveAllByUserIdAsync(userId);
        }
        
        [Test]
        public async Task CallsUnitOfWork_SaveChangesAsync()
        {
            // Arrange
            var userId = 1;
            
            // Act
            var result = await _authService.RevokeRefreshTokensAsync(userId);
            
            // Assert
            await _unitOfWork.Received(1).SaveChangesAsync();
        }
    }
}