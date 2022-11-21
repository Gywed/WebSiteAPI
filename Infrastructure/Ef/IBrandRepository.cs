using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IBrandRepository
{
    DbBrand FetchById(int idBrand);
}