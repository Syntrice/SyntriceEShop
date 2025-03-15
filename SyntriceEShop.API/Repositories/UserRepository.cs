using Microsoft.EntityFrameworkCore;
using SyntriceEShop.API.Database;
using SyntriceEShop.API.Models.UserModel;

namespace SyntriceEShop.API.Repositories;

public class UserRepository(ApplicationDbContext db) : IUserRepository
{
    public User Add(User user)
    {
        var entry = db.Users.Add(user);
        return entry.Entity;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await db.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await db.Users.AnyAsync(user => user.Username == username);
    }
}