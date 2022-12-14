using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Family;

public class UseCaseFetchFamilies:IUseCaseQuery<IEnumerable<DtoOutputFamily>>
{
    private readonly IFamilyRepository _familyRepository;

    public UseCaseFetchFamilies(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public IEnumerable<DtoOutputFamily> Execute()
    {
        var families = _familyRepository.FetchAll();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputFamily>>(families);
    }
}