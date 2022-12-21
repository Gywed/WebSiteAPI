using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Ef;

public class OrderRepository : IOrderRepository
{
    private readonly TakeAndDashContextProvider _contextProvider;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IArticleRepository _articleRepository;

    public OrderRepository(TakeAndDashContextProvider contextProvider, ICategoryRepository categoryRepository,
        IArticleRepository articleRepository)
    {
        _contextProvider = contextProvider;
        _categoryRepository = categoryRepository;
        _articleRepository = articleRepository;
    }

    public DbOrders FetchOrderById(int id)
    {
        using var context = _contextProvider.NewContext();
        var order = context.Orders.FirstOrDefault(u => u.Id == id);

        if (order == null) throw new KeyNotFoundException($"Order with Id {id} has not been found");

        return order;
    }

    public DbOrdersHistory FetchOrderHistoryById(int id)
    {
        using var context = _contextProvider.NewContext();
        var order = context.OrdersHistories.FirstOrDefault(u => u.Id == id);

        if (order == null) throw new KeyNotFoundException($"Order history with Id {id} has not been found");

        return order;
    }

    public IEnumerable<DbOrders> FetchAllByUserId(int idUser)
    {
        using var context = _contextProvider.NewContext();

        var dbOrders = context.Orders.ToList().FindAll(o => o.IdUser == idUser);

        if (dbOrders.IsNullOrEmpty())
            throw new KeyNotFoundException($"The user has no oroder");

        return dbOrders;
    }

    public DbOrders? FindByData(DateTime creationDate, DateTime takeDateTime, int iduser)
    {
        using var context = _contextProvider.NewContext();
        var order = context.Orders.FirstOrDefault(u => u.CreationDate == creationDate 
                                                       && u.TakeDateTime == takeDateTime 
                                                       && u.IdUser == iduser);
        return order;
    }

    public DbOrders FindByOrder(DbOrders orders)
    {
        using var context = _contextProvider.NewContext();
        var order = context.Orders.FirstOrDefault(u => u.CreationDate == orders.CreationDate
                                                        && u.TakeDateTime == orders.TakeDateTime
                                                        && u.IdUser == orders.IdUser);
        if (order == null) throw new ArgumentException("No orders found");
        return order;
    }
    

    public IEnumerable<DbOrders> FetchAllByDate(DateTime date)
    {
        using var context = _contextProvider.NewContext();
        var orders = context.Orders
            .Where(o => o.TakeDateTime.Date == date)
            .ToList();

        if (orders == null) throw new KeyNotFoundException($"No orders to the {date}");

        return orders;
    }

    public IEnumerable<DbOrderHistoryContent> FetchContentByOrderHistory(DbOrdersHistory order)
    {
        using var context = _contextProvider.NewContext();
        var orderContent = context.OrderHistoryContents.Where(o => o.IdOrder == order.Id).AsNoTracking().ToList();
        return orderContent;
    }

    public IEnumerable<DbOrders> FetchAllByUserName(string name)
    {
        using var context = _contextProvider.NewContext();

        var users = context.Users.Where(u => u.Lastname.Contains(name)).ToList();

        if (users.IsNullOrEmpty())
            users = context.Users.Where(u => u.Surname.Contains(name)).ToList();
        else if (users.IsNullOrEmpty())
            throw new ArgumentException($"There are no user matching your input");

        var orders = new List<DbOrders>();

        foreach (var user in users)
        {
            orders.AddRange(context.Orders
                .Where(o => o.IdUser == user.Id)
                .ToList());
        }

        if (orders.IsNullOrEmpty()) throw new KeyNotFoundException($"No orders from this user");

        return orders;
    }

    public IEnumerable<DbOrders> FetchAllByCategoryId(int idCategory)
    {
        using var context = _contextProvider.NewContext();

        var dbArticles = _articleRepository.FetchByCategoryId(idCategory).ToList();

        if (dbArticles.IsNullOrEmpty())
            throw new ArgumentException($"There is no article in this category");

        var dbOrderContents = new List<DbOrderContent>();
        foreach (var dbArticle in dbArticles)
        {
            dbOrderContents.AddRange(context.OrderContents.Where(o => o.IdArticle == dbArticle.Id).ToList());
        }

        var dbOrders = dbOrderContents
            .Select(dbOrderContent => context.Orders.FirstOrDefault(o => o.Id == dbOrderContent.IdOrder)).DistinctBy(orders => orders.Id).ToList();

        return dbOrders;
    }


    public IEnumerable<DbOrderContent> FetchContentByOrder(DbOrders order)
    {
        using var context = _contextProvider.NewContext();
        var orderContent = context.OrderContents.Where(o => o.IdOrder == order.Id).AsNoTracking().ToList();
        return orderContent;
    }

    public DbOrders CreateOrders(DateTime takedatetime, int userid)
    {
        using var context = _contextProvider.NewContext();

        
        var order = new DbOrders
        {
            CreationDate = DateTime.Today.Date,
            TakeDateTime = takedatetime,
            IdUser = userid
        };
        var newOrder =context.Orders.Add(order);
        context.SaveChanges();

        return newOrder.Entity;

    }
    

    public DbOrderContent CreateOrderContent(decimal quantity, int orderid, int idarticle, bool prepared)
    {
        using var context = _contextProvider.NewContext();

        
        var orderContent = new DbOrderContent
        {
            Quantity = quantity,
            IdOrder = orderid,
            IdArticle = idarticle,
            Prepared = prepared
        };
        context.OrderContents.Add(orderContent);
        context.SaveChanges();
        return orderContent;
    }

    public DbOrdersHistory CreateOrdersHistory(DbOrdersHistory dbOrdersHistory)
    {
        using var context = _contextProvider.NewContext();

        context.OrdersHistories.Add(dbOrdersHistory);
        context.SaveChanges();
        return dbOrdersHistory;
    }

    public void CreateOrdersHistoryContent(DbOrderHistoryContent dbOrdersHistoryContent)
    {
        using var context = _contextProvider.NewContext();

        context.OrderHistoryContents.Add(dbOrdersHistoryContent);
        context.SaveChanges();
    }

    public bool UpdateOrderContentPrepared(int orderid, int articleid, bool prepared)
    {
        using var context = _contextProvider.NewContext();

        var orderContent =
            context.OrderContents.FirstOrDefault(o => o.IdOrder == orderid && o.IdArticle == articleid);
        if (orderContent == null)
            throw new KeyNotFoundException($"No order content in order with Id {orderid} and article with Id {articleid}");

        orderContent.Prepared = prepared;
        context.SaveChanges();

        return prepared;
    }
}