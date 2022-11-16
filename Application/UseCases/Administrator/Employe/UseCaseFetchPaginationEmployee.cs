using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Guest.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Employe;

public class UseCaseFetchPaginationEmployee:IUseCaseParameterizedQuery<IEnumerable<DtoOutputUser>, DtoInputPaginationFilteringParameters>
{
    private IUserRepository _userRepository;

    public UseCaseFetchPaginationEmployee(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<DtoOutputUser> Execute(DtoInputPaginationFilteringParameters dto)
    {
        var nbPage = dto.nbPage ?? 1;
        var nbElementsByPage = dto.nbElementsByPage ?? 10;
        if (nbPage<1)
        {
            throw new ArgumentException($"nbPage must be above 0");
        }
        var dbUser = _userRepository.FetchPaginationEmployee(nbPage, nbElementsByPage);
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputUser>>(dbUser);
    }
}