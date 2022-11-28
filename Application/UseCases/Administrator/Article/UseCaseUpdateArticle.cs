using Application.UseCases.Administrator.Dtos;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Administrator.Article;

public class UseCaseUpdateArticle
{
    private IArticleRepository _articleRepository;

    public UseCaseUpdateArticle(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public bool Execute(DbArticle input)
    {
        return _articleRepository.Update(input);
    }
}