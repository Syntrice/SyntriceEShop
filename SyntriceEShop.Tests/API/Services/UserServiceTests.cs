using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Shouldly;
using SyntriceEShop.API.Repositories;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.UserServices;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.Tests.API.Services;

public class UserServiceTests
{
    private IUserRepository _repository;
    private IUnitOfWork _unitOfWork;
    private UserService _userService;
    private IPasswordHasher _passwordHasher;
    
    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<IUserRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _userService = new UserService(_repository, _unitOfWork, _passwordHasher);
    }

    [Test]
    public async Task RegisterAsync_CallsUnitOfWork_SaveChangesAsync()
    {
        // Arrange
        var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
        
        // Act
        await _userService.RegisterAsync(userRegisterDTO);
        
        // Assert
        await _unitOfWork.Received(1).SaveChangesAsync();
    }

    [Test]
    public async Task RegisterAsync_CallsRepository_Add()
    {
        // Arrange
        var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
        
        // Act
        await _userService.RegisterAsync(userRegisterDTO);
        
        // Assert
        _repository.Received(1).Add(Arg.Any<User>());
    }
    
    [Test]
    public async Task RegisterAsync_CallsPasswordHasher_Hash()
    {
        // Arrange
        var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
        
        // Act
        await _userService.RegisterAsync(userRegisterDTO);
        
        // Assert
        _passwordHasher.Received(1).Hash(userRegisterDTO.Password);
    }

    [Test]
    public async Task RegisterAsync_CallsRepository_UsernameExistsAsync()
    {
        // Arrange
        var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
        
        // Act
        await _userService.RegisterAsync(userRegisterDTO);
        
        // Assert
        await _repository.Received(1).UsernameExistsAsync(userRegisterDTO.Username);
    }

    [Test]
    public async Task RegisterAsync_WhenRepositoryUsernameExists_ReturnsConflictResponseType()
    {
        // Arrange
        var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
        _repository.UsernameExistsAsync(userRegisterDTO.Username).Returns(true);
        
        // Act
        var response = await _userService.RegisterAsync(userRegisterDTO);
        
        // Assert
        response.Type.ShouldBe(ServiceResponseType.Conflict);
    }
}