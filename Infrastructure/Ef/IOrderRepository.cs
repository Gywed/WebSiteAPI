using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IOrderRepository
{
    IEnumerable<DbOrders> FetchAllByDate(DateTime date);
    IEnumerable<DbOrderContent> FetchContentByOrder(DbOrders order);
    IEnumerable<DbOrders> FetchAllByUserName(string name);
    IEnumerable<DbOrders> FetchAllByCategoryId(int categoryId);
    DbOrders FetchById(int id);
    DbOrders CreateOrders(DateTime takedatetime, int userid);
    DbOrderContent CreateOrderContent(decimal quantity, int orderid, int idarticle);
}