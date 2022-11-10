using Application.UseCases.Guest.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Employe;

public class UseCaseFetchPaginationEmployee:IUseCaseQuery<DtoOutputUser>
{
    private IUserRepository _userRepository;
    public DtoOutputUser Execute()
    {
        return null;
    }
}