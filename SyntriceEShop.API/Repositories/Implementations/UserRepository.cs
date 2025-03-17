using Microsoft.EntityFrameworkCore;
using SyntriceEShop.API.Database;
using SyntriceEShop.API.Models.UserModel;
using SyntriceEShop.API.Repositories.Interfaces;

namespace SyntriceEShop.API.Repositories.Implementations;

public class UserRepository(ApplicationDbContext db) : GenericRepository<User, int>(db), IUserRepository
{
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await db.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await db.Users.AnyAsync(user => user.Username == username);
    }
}