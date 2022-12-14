using System.Runtime.CompilerServices;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Family;

public class UseCaseFetchArticlesOfFamily:IUseCaseWriter<IEnumerable<DtoOutputArticle>, int>
{
    private readonly IFamilyRepository _familyRepository;

    public UseCaseFetchArticlesOfFamily(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public IEnumerable<DtoOutputArticle> Execute(int idFamily)
    {
        var articlesOfFamily = _familyRepository.FetchArticlesOfFamily(idFamily);
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputArticle>>(articlesOfFamily);
    }
}