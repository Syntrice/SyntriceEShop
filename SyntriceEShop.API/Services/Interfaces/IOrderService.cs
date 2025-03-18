using SyntriceEShop.API.Models.OrderModel.DTO;

namespace SyntriceEShop.API.Services.Interfaces;

public interface IOrderService
{
    Task<ServiceObjectResponse<IEnumerable<GetOrderResponse>>> GetAllOrdersAsync();
    Task<ServiceObjectResponse<GetOrderResponse>> GetOrderByIdAsync(int id);
    Task<ServiceObjectResponse<int>> AddOrderAsync(AddOrderRequest addOrderRequest);
    Task<ServiceResponse> DeleteOrderByIdAsync(int id);
}