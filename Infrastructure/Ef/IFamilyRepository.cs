using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IFamilyRepository
{
    DbFamily Create(string familyName);
    bool Delete(int id);
    bool Update(DbFamily family);
    IEnumerable<DbFamily> FetchAll();
}