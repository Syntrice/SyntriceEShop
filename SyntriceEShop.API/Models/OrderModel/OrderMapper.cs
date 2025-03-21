using SyntriceEShop.API.Models.OrderModel.DTO;

namespace SyntriceEShop.API.Models.OrderModel;

public static class OrderMapper
{
    public static Order ToOrder(this AddOrderRequest request)
    {
        return new Order
        {
            UserId = request.UserId,
            TotalPrice = request.TotalPrice,
            CreatedOnUTC = request.CreatedOnUTC
        };
    }

    public static GetOrderResponse ToGetOrderResponse(this Order order)
    {
        return new GetOrderResponse
        {
            Id = order.Id,
            UserId = order.UserId,
            TotalPrice = order.TotalPrice,
            CreatedOnUTC = order.CreatedOnUTC
        };
    }
}