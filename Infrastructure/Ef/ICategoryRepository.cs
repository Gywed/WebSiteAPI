using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface ICategoryRepository
{
    IEnumerable<DbCategory> FetchAll();
    DbCategory FetchById(int id);
    
    
}