using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Administrator.Family;

public class UseCaseCreateFamily: IUseCaseWriter<DtoOutputFamily, DtoInputCreateFamily>
{
    private IFamilyRepository _familyRepository;

    public UseCaseCreateFamily(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public DtoOutputFamily Execute(DtoInputCreateFamily dto)
    {
        var dbFamily = _familyRepository.Create(dto.family_name);

        return Mapper.GetInstance().Map<DtoOutputFamily>(dbFamily);
    }
}