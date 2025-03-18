using SyntriceEShop.API.Models.OrderModel.DTO;
using SyntriceEShop.API.Services.Interfaces;

namespace SyntriceEShop.API.Services.Implementations;

public class OrderService : IOrderService
{
    public async Task<ServiceObjectResponse<IEnumerable<GetOrderResponse>>> GetAllOrdersAsync()
    {
        return new ServiceObjectResponse<IEnumerable<GetOrderResponse>>();
    }

    public async Task<ServiceObjectResponse<GetOrderResponse>> GetOrderByIdAsync(int id)
    {
        return new ServiceObjectResponse<GetOrderResponse>();
    }

    public async Task<ServiceObjectResponse<int>> AddOrderAsync(AddOrderRequest addOrderRequest)
    {
        return new ServiceObjectResponse<int>();
    }

    public async Task<ServiceResponse> DeleteOrderByIdAsync(int id)
    {
        return new ServiceResponse();
    }
}