using Application.UseCases.dtosGlobal;
using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;

namespace Application.UseCases.Employe;

public class UseCaseConsultOrderByBothDateAndUser : IUseCaseParameterizedQuery<IEnumerable<DtoOutputOrder>,DtoInputOrderFiltering>
{
    private readonly UseCaseConsultOrderOnDate _useCaseConsultOrderOnDate;
    private readonly UseCaseConsultOrderByUser _useCaseConsultOrderByUser;

    public UseCaseConsultOrderByBothDateAndUser(UseCaseConsultOrderOnDate useCaseConsultOrderOnDate, UseCaseConsultOrderByUser useCaseConsultOrderByUser)
    {
        _useCaseConsultOrderOnDate = useCaseConsultOrderOnDate;
        _useCaseConsultOrderByUser = useCaseConsultOrderByUser;
    }

    public IEnumerable<DtoOutputOrder> Execute(DtoInputOrderFiltering dto)
    {
        var hasDate = dto.date.HasValue;
        var hasName = dto.name is { Length: > 0 };

        switch (hasDate)
        {
            case false when !hasName:
                throw new ArgumentException($"No input given");
            case true when !hasName:
                return _useCaseConsultOrderOnDate.Execute(dto.date.Value);
            case false when hasName:
                return _useCaseConsultOrderByUser.Execute(dto.name);
            default:
            {
                var orders = _useCaseConsultOrderByUser.Execute(dto.name);
                orders = orders.Where(o => o.TakeDateTime.Date.CompareTo(dto.date.Value.Date) == 0);

                return orders;
            }
        }
    }
}