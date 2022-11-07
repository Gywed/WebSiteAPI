using Application.UseCases.Utils;
using Infrastructure.Ef;


namespace Application.UseCases.Administrator.Employe;

public class UseCaseDeleteEmploye: IUseCaseParameterizedQuery<bool, int>
{
    private IUserRepository _userRepository;

    public UseCaseDeleteEmploye(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Execute(int param)
    {
        return _userRepository.Delete(param);
    }
}