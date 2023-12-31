using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Infrastructure.Ef;

public class BrandRepository : IBrandRepository
{
    private readonly TakeAndDashContextProvider _contextProvider;

    public BrandRepository(TakeAndDashContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public DbBrand FetchById(int idBrand)
    {
        using var context = _contextProvider.NewContext();
        var dbBrand = context.Brands.FirstOrDefault(b => b.Id == idBrand);

        if (dbBrand == null)
            throw new ArgumentException($"There is no brand with the Id {idBrand}");

        return dbBrand;
    }
    
    public IEnumerable<DbBrand> FetchAll()
    {
        using var context = _contextProvider.NewContext();

        return context.Brands.ToList();
    }
}