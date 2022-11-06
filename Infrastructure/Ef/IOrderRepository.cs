using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IOrderRepository
{
    IEnumerable<DbOrders> FetchAllByDate();
}