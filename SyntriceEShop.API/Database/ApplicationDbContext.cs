using Microsoft.EntityFrameworkCore;
using SyntriceEShop.Common.Models.UserModel;

namespace SyntriceEShop.API.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}