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
}