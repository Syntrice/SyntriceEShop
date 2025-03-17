using SyntriceEShop.API.Database;
using SyntriceEShop.API.Models.ShoppingCartModel;
using SyntriceEShop.API.Repositories.Interfaces;

namespace SyntriceEShop.API.Repositories.Implementations;

public class ShoppingCartRepository(ApplicationDbContext db)
    : GenericRepository<ShoppingCart, int>(db), IShoppingCartRepository
{
}