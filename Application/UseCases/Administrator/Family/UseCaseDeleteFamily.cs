using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Family;

public class UseCaseDeleteFamily: IUseCaseWriter<DtoOutputFamily, DtoInputDeleteFamily>
{
    private readonly IFamilyRepository _familyRepository;

    public UseCaseDeleteFamily(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public DtoOutputFamily Execute(DtoInputDeleteFamily dto)
    {
        return Mapper.GetInstance().Map<DtoOutputFamily>(_familyRepository.Delete(dto.Id));
    }
}