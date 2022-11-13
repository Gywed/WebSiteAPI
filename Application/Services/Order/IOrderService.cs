using Infrastructure.Ef.DbEntities;

namespace Application.Services.Order;

public interface IOrderService
{
    Domain.Order Fetch(DbOrders order);
}