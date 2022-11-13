using Infrastructure.Ef;

namespace Application.Services.Article;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public Domain.Article FetchById(int articleId)
    {
        var dbArticle = _articleRepository.FetchById(articleId);

        return Mapper.GetInstance().Map<Domain.Article>(dbArticle);
    }
}