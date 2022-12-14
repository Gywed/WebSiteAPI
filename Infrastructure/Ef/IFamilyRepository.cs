using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IFamilyRepository
{
    DbFamily Create(string familyName);
    bool Delete(int id);
}