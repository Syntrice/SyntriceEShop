using SyntriceEShop.API.Repositories;
using SyntriceEShop.API.Services.Response;
using SyntriceEShop.API.Utilities;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Services;

public class UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    : IUserService
{
    // for testing purpose for now return the user
    public async Task<ServiceObjectResponse<User>> RegisterAsync(UserRegisterDTO userRegisterDTO)
    {
        if (await userRepository.UsernameExistsAsync(userRegisterDTO.Username))
        {
            return new ServiceObjectResponse<User>()
            {
                Type = ServiceResponseType.Conflict,
                Message = $"User with username {userRegisterDTO.Username} already exists."
            };
        }

        var user = new User()
        {
            Username = userRegisterDTO.Username,
            PasswordHash = passwordHasher.Hash(userRegisterDTO.Password)
        };

        var created = userRepository.Add(user);
        await unitOfWork.SaveChangesAsync();

        return new ServiceObjectResponse<User>()
            { Type = ServiceResponseType.Success, Value = created, Message = "Successfully registered user." };
    }

    // for testing purpose for now return the user
    public async Task<ServiceObjectResponse<User>> LoginAsync(UserLoginDTO userLoginDTO)
    {
        User? user = await userRepository.GetUserByUsernameAsync(userLoginDTO.Username);

        if (user == null)
        {
            return new ServiceObjectResponse<User>() { Type = ServiceResponseType.NotFound, Message = $"User {userLoginDTO.Username} does not exist." };
        }

        bool verified = passwordHasher.Verify(userLoginDTO.Password, user.PasswordHash);

        if (!verified)
        {
            return new ServiceObjectResponse<User>() { Type = ServiceResponseType.InvalidCredentials, Message = "Password is not valid." };
        }

        return new ServiceObjectResponse<User>() { Type = ServiceResponseType.Success, Value = user };
    }
}