using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Repositories;

public interface IUserRepository
{
    User Add(User user);
}