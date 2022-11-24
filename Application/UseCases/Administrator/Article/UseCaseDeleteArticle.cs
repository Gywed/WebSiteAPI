using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Article;

public class UseCaseDeleteArticle
{
    private IArticleRepository _articleRepository;

    public UseCaseDeleteArticle(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public bool Execute(int param)
    {
        return _articleRepository.Delete(param);
    }
}