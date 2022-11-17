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
        var order = context.Orders.FirstOrDefault(u => u.Id == id);

        if (order == null) throw new KeyNotFoundException($"Order with id {id} has not been found");

        return order;
    }
    
    public IEnumerable<DbOrders> FetchAllByDate(DateTime date)
    {
        using var context = _contextProvider.NewContext();
        var orders = context.Orders
            .Where(o => o.CreationDate == date)
            .ToList();

        if (orders == null) throw new KeyNotFoundException($"No orders to the {date}");

        return orders;
    }
    
    public IEnumerable<DbOrders> FetchAllByUserName(string name)
    {
        using var context = _contextProvider.NewContext();

        var user = context.Users.FirstOrDefault(u => u.lastname.Contains(name));

        if (user == null)
            user = context.Users.FirstOrDefault(u => u.surname.Contains(name));
        else if (user == null)
            throw new ArgumentException($"There is no user matching your input");
        
        var orders = context.Orders
            .Where(o => o.IdUser == user!.id)
            .ToList();

        if (orders == null) throw new KeyNotFoundException($"No orders from this user");

        return orders;
    }

    public IEnumerable<DbOrderContent> FetchContentByOrder(DbOrders order)
    {
        using var context = _contextProvider.NewContext();
        var orderContent = context.OrderContents.Where(o => o.idorder == order.Id).ToList();
        return orderContent;
    }
}