using Application.UseCases.Client.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseFetchArticleById:IUseCaseParameterizedQuery<DtoOutputArticle,int>
{
    private readonly IArticleRepository _articleRepository;

    public UseCaseFetchArticleById(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }


  
    public DtoOutputArticle Execute(int id)
    {
        var articles = _articleRepository.FetchById(id);
        return Mapper.GetInstance().Map<DtoOutputArticle>(articles);
    }
}