using System.Runtime.CompilerServices;
using Application.Services.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Family;

public class UseCaseFetchArticlesOfFamily:IUseCaseWriter<IEnumerable<DtoOutputArticle>, int>
{
    private readonly IArticleService _articleService;
    private readonly IFamilyRepository _familyRepository;

    public UseCaseFetchArticlesOfFamily(IFamilyRepository familyRepository, IArticleService articleService)
    {
        _familyRepository = familyRepository;
        _articleService = articleService;
    }

    public IEnumerable<DtoOutputArticle> Execute(int idFamily)
    {
        var articlesOfFamily = _familyRepository.FetchArticlesOfFamily(idFamily);
        var articles = articlesOfFamily.Select(articleOfFamily => _articleService.FetchById(articleOfFamily.Id)).ToList();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputArticle>>(articles);
    }
}