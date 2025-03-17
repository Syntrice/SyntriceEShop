using SyntriceEShop.API.Database;
using SyntriceEShop.API.Models.ProductCategoryModel;
using SyntriceEShop.API.Repositories.Interfaces;

namespace SyntriceEShop.API.Repositories.Implementations;

public class ProductCategoryRepository(ApplicationDbContext db)
    : GenericRepository<ProductCategory, int>(db), IProductCategoryRepository
{
}