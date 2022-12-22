using Application.Services.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseFetchArticleByCategory:IUseCaseParameterizedQuery<IEnumerable<DtoOutputArticle>,int>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleService _articleService;

    public UseCaseFetchArticleByCategory(IArticleRepository articleRepository, IArticleService articleService)
    {
        _articleRepository = articleRepository;
        _articleService = articleService;
    }

    public IEnumerable<DtoOutputArticle> Execute(int idCategory)
    {
        var dbArticle = _articleRepository.FetchByCategoryId(idCategory)
            .Select(article => _articleService.FetchById(article.Id)).ToList();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputArticle>>(dbArticle);
    }
}