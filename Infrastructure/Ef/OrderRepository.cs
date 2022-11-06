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

    public IEnumerable<DbOrders> FetchAllToday()
    {
        var date = DateTime.Today.ToString("d/M/y");
        using var context = _contextProvider.NewContext();
        var orders = context.Orders
            .Where(o => o.creationDate == date)
            .ToList();

        if (orders == null) throw new KeyNotFoundException($"No order today");

        return orders;
    }

    public IEnumerable<IEnumerable<DbOrderContent>> FetchContentByOrders()
    {
        var orders = FetchAllToday();
        using var context = _contextProvider.NewContext();
        List<List<DbOrderContent>> ordersContents = null;
        foreach (var order in orders)
        {
            ordersContents.Append(context.OrderContents.Where(o => o.idorder == order.id).ToList());
        }
        return ordersContents;
    }
}