using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.Repositories;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.UserServices;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.Tests.API.Services;

[TestFixture]
public class UserServiceTests
{
    private IUserRepository _repository;
    private IUnitOfWork _unitOfWork;
    private IPasswordHasher _passwordHasher;
    private ITokenProvider _tokenProvider;
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<IUserRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _tokenProvider = Substitute.For<ITokenProvider>();
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
        public async Task CallsPasswordHasher_Hash()
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
        
    }
}