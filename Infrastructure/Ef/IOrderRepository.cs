using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IOrderRepository
{
    IEnumerable<DbOrders> FetchAllByDate(DateTime date);
    IEnumerable<DbOrderContent> FetchContentByOrder(DbOrders order);
    IEnumerable<DbOrders> FetchAllByUserName(string name);
    IEnumerable<DbOrders> FetchAllByCategory(string category);
    DbOrders FetchById(int id);
}