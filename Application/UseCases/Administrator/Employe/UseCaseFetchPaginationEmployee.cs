using Application.UseCases.Administrator.Dtos;
using Application.UseCases.dtosGlobal;
using Application.UseCases.Guest.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Administrator.Employe;

public class UseCaseFetchPaginationEmployee:IUseCaseParameterizedQuery<DtoOutputPaginationFiltering<DtoOutputUser>,
    DtoInputEmployeeFilteringParameters>
{
    private IUserRepository _userRepository;

    public UseCaseFetchPaginationEmployee(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public DtoOutputPaginationFiltering<DtoOutputUser> Execute(DtoInputEmployeeFilteringParameters dto)
    {
        var surname = dto.Surname ?? "";
        var lastname = dto.Lastname ?? "";
        var nbPage = dto.DtoPagination.NbPage ?? 1;
        var nbElementsByPage = dto.DtoPagination.NbElementsByPage ?? 10;
        if (nbPage<1)
        {
            throw new ArgumentException($"nbPage must be above 0");
        }
        var pageElements = _userRepository.FetchEmployeesFilteredPagination(nbPage, nbElementsByPage, surname, lastname);
        var totalElements = _userRepository.FetchEmployeesFilteringCount(surname, lastname);
        var nbOfPages = Math.Ceiling((decimal)totalElements / nbElementsByPage);

        return new DtoOutputPaginationFiltering<DtoOutputUser>
        {
            PageElements = Mapper.GetInstance().Map<IEnumerable<DtoOutputUser>>(pageElements),
            NbOfPages = (int)nbOfPages
        };
    }
}