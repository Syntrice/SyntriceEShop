using SyntriceEShop.API.Database;
using SyntriceEShop.API.Models.OrderModel;
using SyntriceEShop.API.Repositories.Interfaces;

namespace SyntriceEShop.API.Repositories.Implementations;

public class OrderRepository(ApplicationDbContext db) : GenericRepository<Order, int>(db), IOrderRepository 
{
}