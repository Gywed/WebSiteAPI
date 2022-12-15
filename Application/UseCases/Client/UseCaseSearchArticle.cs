using Application.Services.Article;
using Application.UseCases.Utils;
using Domain;
using Infrastructure.Ef;
using DtoOutputArticle = Application.UseCases.Administrator.Article.Dtos.DtoOutputArticle;

namespace Application.UseCases.Client;

public class UseCaseSearchArticle : IUseCaseParameterizedQuery<IEnumerable<DtoOutputArticle>,string>
{
    private readonly IArticleService _articleService;
    private readonly IArticleRepository _articleRepository;

    public UseCaseSearchArticle(IArticleRepository articleRepository, IArticleService articleService)
    {
        _articleRepository = articleRepository;
        _articleService = articleService;
    }

    public IEnumerable<DtoOutputArticle> Execute(string name)
    {
        var dbArticles = _articleRepository.FetchByName(name);
        var articles = dbArticles.Select(dbArticle => _articleService.FetchById(dbArticle.Id)).ToList();

        return Mapper.GetInstance().Map<IEnumerable<DtoOutputArticle>>(articles);
    }
}