using SyntriceEShop.API.Repositories;
using SyntriceEShop.API.Services.Response;
using SyntriceEShop.API.Utilities;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Services;

public class UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher) : IUserService
{
    // for testing purpose for now return the user
    public async Task<ServiceObjectResponse<User>> RegisterAsync(UserRegisterDTO userRegisterDTO)
    {
        var user = new User()
        {
            Username = userRegisterDTO.Username,
            PasswordHash = passwordHasher.Hash(userRegisterDTO.Password)
        };
        
        var created = userRepository.Add(user);
        await unitOfWork.SaveChangesAsync();

        return new ServiceObjectResponse<User>() { Type = ServiceResponseType.Success, Value = created, Message = "Successfully registered user."};
    }
}