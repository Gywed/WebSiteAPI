using System.Collections;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Article;

public class UseCaseFetchAllArticle:
    IUseCaseQuery<IEnumerable<DtoOutputArticle>>
{
    private readonly IArticleRepository _articleRepository;

    public UseCaseFetchAllArticle(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public IEnumerable<DtoOutputArticle> Execute()
    {
        var articles = _articleRepository.FetchAll();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputArticle>>(articles);
    }
}