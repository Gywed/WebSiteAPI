using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Family;

public class UseCaseFetchFamiliesOfArticle:IUseCaseWriter<IEnumerable<DtoOutputFamily>, int>
{
    private readonly IFamilyRepository _familyRepository;

    public UseCaseFetchFamiliesOfArticle(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public IEnumerable<DtoOutputFamily> Execute(int idArticle)
    {
        var families = _familyRepository.FetchFamiliesOfArticle(idArticle);
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputFamily>>(families);
    }
}