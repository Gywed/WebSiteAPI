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

        if (!hasDate && !hasName)
            throw new ArgumentException($"No input given");
        if (hasDate && !hasName)
            return _useCaseConsultOrderOnDate.Execute(dto.date.Value);
        if (!hasDate && hasName)
            return _useCaseConsultOrderByUser.Execute(dto.name);
        else
        {
            var orders = _useCaseConsultOrderByUser.Execute(dto.name);
            orders = orders.Where(o => o.Date.CompareTo(dto.date.Value) == 0);

            return orders;
        }
    }
}