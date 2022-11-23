using Application.UseCases.dtosGlobal;
using Application.UseCases.Guest.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Administrator.Employe;

public class UseCaseFetchPaginationEmployee:IUseCaseParameterizedQuery<DtoOutputPaginationFiltering<DtoOutputUser>,
    DtoInputPaginationFilteringParameters>
{
    private IUserRepository _userRepository;

    public UseCaseFetchPaginationEmployee(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public DtoOutputPaginationFiltering<DtoOutputUser> Execute(DtoInputPaginationFilteringParameters dto)
    {
        var nbPage = dto.nbPage ?? 1;
        var nbElementsByPage = dto.nbElementsByPage ?? 10;
        if (nbPage<1)
        {
            throw new ArgumentException($"nbPage must be above 0");
        }
        var pageElements = _userRepository.FetchEmployeesFilteredPagination(nbPage, nbElementsByPage, null, null);
        var totalElements = _userRepository.FetchEmployeesFilteringCount(null, null);
        var nbOfPages = Math.Ceiling((decimal)totalElements / nbElementsByPage);

        return new DtoOutputPaginationFiltering<DtoOutputUser>
        {
            pageElements = Mapper.GetInstance().Map<IEnumerable<DtoOutputUser>>(pageElements),
            nbOfPages = (int)nbOfPages
        };
    }
}