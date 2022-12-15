using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Administrator.Family;

public class UseCaseUpdateFamily:IUseCaseWriter<bool, DtoInputUpdateFamily>
{
    private readonly IFamilyRepository _familyRepository;

    public UseCaseUpdateFamily(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public bool Execute(DtoInputUpdateFamily dto)
    {
        return _familyRepository.Update(new DbFamily
        {
            Id = dto.Id,
            FamilyName = dto.FamilyName
        });
    }
}