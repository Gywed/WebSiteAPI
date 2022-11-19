using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Infrastructure.Ef;

public class ArticleRepository : IArticleRepository
{
    private readonly TakeAndDashContextProvider _contextProvider;

    public ArticleRepository(TakeAndDashContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public DbArticle FetchById(int id)
    {
        using var context = _contextProvider.NewContext();
        var article = context.Articles.FirstOrDefault(a => a.Id == id);
        
        if (article == null)
            throw new KeyNotFoundException($"Article with id {id} has not been found");

        return article;
    }

    public IEnumerable<DbArticle> FetchAll()
    {
        using var context = _contextProvider.NewContext();
        return context.Articles.ToList();
    }

    public IEnumerable<DbArticle> FetchByName(string name)
    {
        using var context = _contextProvider.NewContext();
        return context.Articles.Where(a => a.Nametag == name).ToList();
        
    }

    public IEnumerable<DbArticle> FetchByCategoryId(int idCategory)
    {
        using var context = _contextProvider.NewContext();
        return context.Articles.Where(a => a.IdCategory == idCategory).ToList();
    }
}