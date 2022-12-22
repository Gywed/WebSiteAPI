using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Article;

public class UseCaseDeleteArticle
{
    private IArticleRepository _articleRepository;

    public UseCaseDeleteArticle(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public bool Execute(int id)
    {
        var dbArticle = _articleRepository.FetchById(id);
        if (dbArticle.ImagePath != "assets/articles/No-Image-Placeholder.jpg")
            File.Delete(dbArticle.ImagePath);
        return _articleRepository.Delete(id);
    }
}