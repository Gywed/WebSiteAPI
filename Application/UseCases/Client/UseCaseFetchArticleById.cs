using Application.Services.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Client.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseFetchArticleById:IUseCaseParameterizedQuery<DtoOutputArticle,int>
{
    private readonly IArticleService _articleService;
    private readonly IArticleRepository _articleRepository;

    public UseCaseFetchArticleById(IArticleRepository articleRepository, IArticleService articleService)
    {
        _articleRepository = articleRepository;
        _articleService = articleService;
    }


  
    public DtoOutputArticle Execute(int id)
    {
        var dbArticle = _articleRepository.FetchById(id);
        return Mapper.GetInstance().Map<DtoOutputArticle>(_articleService.FetchById(dbArticle.Id));
    }
}