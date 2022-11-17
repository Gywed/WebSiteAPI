using Application.UseCases.Utils;
using Infrastructure.Ef;
using DtoOutputArticle = Application.UseCases.Administrator.Article.Dtos.DtoOutputArticle;

namespace Application.UseCases.Client;

public class UseCaseSearchArticle : IUseCaseParameterizedQuery<IEnumerable<DtoOutputArticle>,string>
{
    private readonly IArticleRepository _articleRepository;

    public UseCaseSearchArticle(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public IEnumerable<DtoOutputArticle> Execute(string name)
    {
        var articles = _articleRepository.FetchByName(name);
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputArticle>>(articles);
    }
}