using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IOrderRepository
{
    IEnumerable<DbOrders> FetchAllByDate(DateTime date);
    IEnumerable<DbOrderContent> FetchContentByOrder(DbOrders order);
    IEnumerable<DbOrderHistoryContent> FetchContentByOrderHistory(DbOrdersHistory order);
    IEnumerable<DbOrders> FetchAllByUserName(string name);
    IEnumerable<DbOrders> FetchAllByCategoryId(int categoryId);
    DbOrders FetchOrderById(int id);
    DbOrdersHistory FetchOrderHistoryById(int id);
    IEnumerable<DbOrders> FetchAllByUserId(int idUser);
    DbOrders CreateOrders(DateTime takedatetime, int userid);
    DbOrderContent CreateOrderContent(decimal quantity, int orderid, int idarticle, bool prepared);
    DbOrdersHistory CreateOrdersHistory(DbOrdersHistory dbOrdersHistory);
    void DeleteOrder(int idOrder);
    void DeleteOrdersContent(int idOrder);
    void CreateOrdersHistoryContent(DbOrderHistoryContent dbOrdersHistory);
    bool UpdateOrderContentPrepared(int orderid, int articleid, bool prepared);
}