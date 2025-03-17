using SyntriceEShop.API.Database;
using SyntriceEShop.API.Models.ProductModel;
using SyntriceEShop.API.Repositories.Interfaces;

namespace SyntriceEShop.API.Repositories.Implementations;

public class ProductRepository(ApplicationDbContext db) : GenericRepository<Product, int>(db), IProductRepository
{
}