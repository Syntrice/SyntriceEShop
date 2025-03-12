using Microsoft.AspNetCore.Identity;
using NSubstitute;
using SyntriceEShop.API.Repositories;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Utilities;
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
    public async Task RegisterAsync_ShouldCall_UnitOfWork_SaveChangesAsync()
    {
        // Arrange
        var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
        
        // Act
        await _userService.RegisterAsync(userRegisterDTO);
        
        // Assert
        await _unitOfWork.Received(1).SaveChangesAsync();
    }

    [Test]
    public async Task RegisterAsync_ShouldCall_Repository_Add()
    {
        // Arrange
        var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
        
        // Act
        await _userService.RegisterAsync(userRegisterDTO);
        
        // Assert
        _repository.Received(1).Add(Arg.Any<User>());
    }
    
    [Test]
    public async Task RegisterAsync_ShouldCall_PasswordHasher_Hash()
    {
        // Arrange
        var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
        
        // Act
        await _userService.RegisterAsync(userRegisterDTO);
        
        // Assert
        _passwordHasher.Received(1).Hash(userRegisterDTO.Password);
    }
}