using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Infrastructure.Ef;

public class OrderRepository : IOrderRepository
{
    private readonly TakeAndDashContextProvider _contextProvider;

    public OrderRepository(TakeAndDashContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public DbOrders FetchById(int id)
    {
        using var context = _contextProvider.NewContext();
        var order = context.Orders.FirstOrDefault(u => u.id == id);

        if (order == null) throw new KeyNotFoundException($"Order with id {id} has not been found");

        return order;
    }
    
    public IEnumerable<DbOrders> FetchAllByDate(string date)
    {
        using var context = _contextProvider.NewContext();
        var orders = context.Orders
            .Where(o => o.creationDate == date)
            .ToList();

        if (orders == null) throw new KeyNotFoundException($"No orders to the {date}");

        return orders;
    }

    public IEnumerable<DbOrderContent> FetchContentByOrder(DbOrders order)
    {
        using var context = _contextProvider.NewContext();
        var orderContent = context.OrderContents.Where(o => o.idorder == order.id).ToList();
        return orderContent;
    }
}