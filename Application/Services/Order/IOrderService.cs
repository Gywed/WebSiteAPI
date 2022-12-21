using Infrastructure.Ef.DbEntities;

namespace Application.Services.Order;

public interface IOrderService
{
    Domain.Order FetchOrder(DbOrders order);
    Domain.OrderHistory FetchOrderHistory(DbOrdersHistory order);
}