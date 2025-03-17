using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User, int>
{
    User Add(User user);
    Task<bool> UsernameExistsAsync(string username);
    Task<User?> GetUserByUsernameAsync(string username);
}