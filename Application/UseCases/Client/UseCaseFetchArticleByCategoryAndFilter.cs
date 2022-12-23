using Application.Services.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseFetchArticleByCategoryAndFilter
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleService _articleService;

    public UseCaseFetchArticleByCategoryAndFilter(IArticleRepository articleRepository, IArticleService articleService)
    {
        _articleRepository = articleRepository;
        _articleService = articleService;
    }

    public IEnumerable<DtoOutputArticle> Execute(string name, int idCategory)
    {
        var dbArticle = _articleRepository.FetchByCategoryIdAndName(name, idCategory)
            .Select(article => _articleService.FetchById(article.Id)).ToList();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputArticle>>(dbArticle);
    }
}