using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Family;

public class UseCaseDeleteFamily: IUseCaseWriter<bool, DtoInputDeleteFamily>
{
    private IFamilyRepository _familyRepository;

    public UseCaseDeleteFamily(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public bool Execute(DtoInputDeleteFamily dto)
    {
        return _familyRepository.Delete(dto.id);
    }
}