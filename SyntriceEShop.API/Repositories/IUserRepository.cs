using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Repositories;

public interface IUserRepository
{
    User Add(User user);
    Task<bool> UsernameExistsAsync(string username);
    Task<User?> GetUserByUsernameAsync(string username);
}