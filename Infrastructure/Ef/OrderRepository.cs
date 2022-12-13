using System.Diagnostics;
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

    public DbOrders FetchById(int id)
    {
        using var context = _contextProvider.NewContext();
        var order = context.Orders.FirstOrDefault(u => u.Id == id);

        if (order == null) throw new KeyNotFoundException($"Order with id {id} has not been found");

        return order;
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

    public IEnumerable<DbOrders> FetchAllByUserName(string name)
    {
        using var context = _contextProvider.NewContext();

        var users = context.Users.Where(u => u.lastname.Contains(name)).ToList();

        if (users.IsNullOrEmpty())
            users = context.Users.Where(u => u.surname.Contains(name)).ToList();
        else if (users.IsNullOrEmpty())
            throw new ArgumentException($"There are no user matching your input");

        var orders = new List<DbOrders>();

        foreach (var user in users)
        {
            orders.AddRange(context.Orders
                .Where(o => o.IdUser == user.id)
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
            dbOrderContents.AddRange(context.OrderContents.Where(o => o.idarticle == dbArticle.Id).ToList());
        }

        var dbOrders = dbOrderContents
            .Select(dbOrderContent => context.Orders.FirstOrDefault(o => o.Id == dbOrderContent.idorder)).DistinctBy(orders => orders.Id).ToList();

        return dbOrders;
    }


    public IEnumerable<DbOrderContent> FetchContentByOrder(DbOrders order)
    {
        using var context = _contextProvider.NewContext();
        var orderContent = context.OrderContents.Where(o => o.idorder == order.Id).AsNoTracking().ToList();
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
            quantity = quantity,
            idorder = orderid,
            idarticle = idarticle,
            prepared = prepared
        };
        context.OrderContents.Add(orderContent);
        context.SaveChanges();
        return orderContent;
    }

    public bool UpdateOrderContentPrepared(int orderid, int articleid, bool prepared)
    {
        using var context = _contextProvider.NewContext();

        var orderContent =
            context.OrderContents.FirstOrDefault(o => (o.idorder == orderid) & (o.idarticle == articleid));
        if (orderContent == null)
            throw new KeyNotFoundException($"No order content in order with id {orderid} and article with id {articleid}");
        
        orderContent.prepared = prepared;
        context.SaveChanges();

        return prepared;
    }
}