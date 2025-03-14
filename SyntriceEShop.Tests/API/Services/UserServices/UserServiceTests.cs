using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using SyntriceEShop.API.Repositories;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.UserServices;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.Tests.API.Services.UserServices;

[TestFixture]
public class UserServiceTests
{
    private IUserRepository _repository;
    private IUnitOfWork _unitOfWork;
    private IPasswordHasher _passwordHasher;
    private IJWTProvider _tokenProvider;
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<IUserRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _tokenProvider = Substitute.For<IJWTProvider>();
        _userService = new UserService(_repository, _unitOfWork, _passwordHasher, _tokenProvider);
    }
    
    [TestFixture]
    public class RegisterAsync : UserServiceTests
    {
        [Test]
        public async Task CallsUnitOfWork_SaveChangesAsync()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };

            // Act
            await _userService.RegisterAsync(userRegisterDTO);

            // Assert
            await _unitOfWork.Received(1).SaveChangesAsync();
        }

        [Test]
        public async Task CallsRepository_Add()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };

            // Act
            await _userService.RegisterAsync(userRegisterDTO);

            // Assert
            _repository.Received(1).Add(Arg.Any<User>());
        }

        [Test]
        public async Task CallsPasswordHasher_Hash_WithRegisterPassword()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };

            // Act
            await _userService.RegisterAsync(userRegisterDTO);

            // Assert
            _passwordHasher.Received(1).Hash(userRegisterDTO.Password);
        }

        [Test]
        public async Task CallsRepository_UsernameExistsAsync()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };

            // Act
            await _userService.RegisterAsync(userRegisterDTO);

            // Assert
            await _repository.Received(1).UsernameExistsAsync(userRegisterDTO.Username);
        }

        [Test]
        public async Task WhenRepositoryUsernameExists_ReturnsConflictResponseType()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
            _repository.UsernameExistsAsync(userRegisterDTO.Username).Returns(true);

            // Act
            var response = await _userService.RegisterAsync(userRegisterDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.Conflict);
        }

        [Test]
        public async Task WhenRepositoryUsernameDoesNotExist_ReturnsSuccessResponseType()
        {
            // Arrange
            var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
            _repository.UsernameExistsAsync(userRegisterDTO.Username).Returns(false);

            // Act
            var response = await _userService.RegisterAsync(userRegisterDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.Success);
        }
    }

    [TestFixture]
    public class LoginAsync : UserServiceTests
    {
        [Test]
        public async Task CallsRepository_GetUserByUsernameAsync_WithCorrectParameters()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO() { Username = "username", Password = "password" };

            // Act
            await _userService.LoginAsync(userLoginDTO);

            // Assert
            await _repository.Received(1).GetUserByUsernameAsync(userLoginDTO.Username);
        }
        
        [Test]
        public async Task CallsPasswordHasher_Verify_WithCorrectParameters()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO() { Username = "username", Password = "password" };
            var userEntity = new User() { Username = "username", PasswordHash = "hashedpassword" };
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).Returns(userEntity);

            // Act
            await _userService.LoginAsync(userLoginDTO);

            // Assert
            _passwordHasher.Received(1).Verify(userLoginDTO.Password, userEntity.PasswordHash);
        }

        [Test]
        public async Task CallsTokenProvider_Create_WithUserEntity()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO() { Username = "username", Password = "password" };
            var userEntity = new User() { Username = "username", PasswordHash = "hashedpassword" };
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).Returns(userEntity);

            // Act
            await _userService.LoginAsync(userLoginDTO);

            // Assert
            _tokenProvider.GenerateJWT(userEntity);
        }

        [Test]
        public async Task WhenRepository_GetByUsernameReturnsNull_ReturnsNotFoundResponseType()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO() { Username = "username", Password = "password" };
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).ReturnsNull();

            // Act
            var response = await _userService.LoginAsync(userLoginDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.NotFound);
        }
        
        [Test]
        public async Task WhenPasswordHasher_VerifyReturnsFalse_ReturnsInvalidCredentialsResponseType()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO() { Username = "username", Password = "password" };
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).ReturnsNull();

            // Act
            var response = await _userService.LoginAsync(userLoginDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.NotFound);
        }

        [Test]
        public async Task WhenAllChecksPass_ReturnsSuccessResponseTypeWithJwtToken()
        {
            // Arrange
            var userLoginDTO = new UserLoginDTO() { Username = "username", Password = "password" };
            var userEntity = new User() { Username = "username", PasswordHash = "hashedpassword" };
            var jwtToken = "jwtToken";
            _repository.GetUserByUsernameAsync(userLoginDTO.Username).Returns(userEntity);
            _passwordHasher.Verify(userLoginDTO.Password, userEntity.PasswordHash).Returns(true);
            _tokenProvider.GenerateJWT(userEntity).Returns(jwtToken);

            // Act
            var response = await _userService.LoginAsync(userLoginDTO);

            // Assert
            response.Type.ShouldBe(ServiceResponseType.Success);
            response.Value.ShouldBe(jwtToken);
        }
    }
}