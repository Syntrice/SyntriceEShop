using Microsoft.EntityFrameworkCore;
using SyntriceEShop.API.Database;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Repositories;

public class UserRepository(ApplicationDbContext db) : IUserRepository
{
    public User Add(User user)
    {
        var entry = db.Users.Add(user);
        return entry.Entity;
    }
}