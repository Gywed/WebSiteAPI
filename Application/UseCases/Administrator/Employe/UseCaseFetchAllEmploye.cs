using Application.UseCases.Guest.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Employe;

public class UseCaseFetchAllEmploye: IUseCaseQuery<IEnumerable<DtoOutputUser>>
{
    private readonly IUserRepository _userRepository;

    public UseCaseFetchAllEmploye(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<DtoOutputUser> Execute()
    {
        var dbUser = _userRepository.FetchAllEmployees();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputUser>>(dbUser);
    }
}
