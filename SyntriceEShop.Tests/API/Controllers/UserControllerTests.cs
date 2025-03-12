using NSubstitute;
using SyntriceEShop.API.Controllers;
using SyntriceEShop.API.Services;
using SyntriceEShop.API.Services.Response;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.Tests.API.Controllers;

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

    [Test]
    public async Task RegisterAsync_CallsUserService_RegisterAsync()
    {
        // Arrange
        var userRegisterDTO = new UserRegisterDTO() { Username = "username", Password = "password" };
        _userService.RegisterAsync(userRegisterDTO).Returns(new ServiceObjectResponse<User>());
        
        // Act
        await _userController.RegisterAsync(userRegisterDTO);
        
        // Assert
        await _userService.Received(1).RegisterAsync(userRegisterDTO);
    }
}