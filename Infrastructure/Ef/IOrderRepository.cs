using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IOrderRepository
{
    IEnumerable<DbOrders> FetchAllByDate(DateTime date);
    IEnumerable<DbOrderContent> FetchContentByOrder(DbOrders order);

    DbOrders FetchById(int id);
}