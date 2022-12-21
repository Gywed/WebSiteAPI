using Application.Services.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Administrator.Family;

public class UseCaseFetchArticlesInSameFamilies: IUseCaseWriter<IEnumerable<DtoOutputArticle>, int>
{
    private readonly IArticleService _articleService;
    private readonly IFamilyRepository _familyRepository;

    public UseCaseFetchArticlesInSameFamilies(IFamilyRepository familyRepository, IArticleService articleService)
    {
        _familyRepository = familyRepository;
        _articleService = articleService;
    }

    public IEnumerable<DtoOutputArticle> Execute(int idArticle)
    {
        var articles =  _familyRepository.FetchArticlesInSameFamilies(idArticle)
            .Select(articleOfFamily => _articleService.FetchById(articleOfFamily.Id)).ToList();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputArticle>>(articles);
    }
}