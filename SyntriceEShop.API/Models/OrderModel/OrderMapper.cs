using SyntriceEShop.API.Models.OrderModel.DTO;

namespace SyntriceEShop.API.Models.OrderModel;

public static class OrderMapper
{
    public static Order ToOrder(this AddOrderRequest request)
    {
        return new Order();
    }

    public static GetOrderResponse ToGetOrderResponse(this Order order)
    {
        return new GetOrderResponse();
    }
}