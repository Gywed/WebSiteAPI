using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Infrastructure.Ef;

public class CategoryRepository : ICategoryRepository
{
    private readonly TakeAndDashContextProvider _contextProvider;

    public CategoryRepository(TakeAndDashContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public IEnumerable<DbCategory> FetchAll()
    {
        using var context = _contextProvider.NewContext();

        return context.Categories.ToList();
    }

    public DbCategory FetchById(int id)
    {
        using var context = _contextProvider.NewContext();

        return context.Categories.FirstOrDefault(c => c.Id == id);
    }
}