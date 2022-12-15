using System.Collections;
using Application.Services.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Article;

public class UseCaseFetchAllArticle:
    IUseCaseQuery<IEnumerable<DtoOutputArticle>>
{
    private readonly IArticleService _articleService;
    private readonly IArticleRepository _articleRepository;

    public UseCaseFetchAllArticle(IArticleRepository articleRepository, IArticleService articleService)
    {
        _articleRepository = articleRepository;
        _articleService = articleService;
    }

    public IEnumerable<DtoOutputArticle> Execute()
    {
        var dbArticles = _articleRepository.FetchAll();

        var articles = dbArticles.Select(dbArticle => _articleService.FetchById(dbArticle.Id)).ToList();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputArticle>>(articles);
    }
}