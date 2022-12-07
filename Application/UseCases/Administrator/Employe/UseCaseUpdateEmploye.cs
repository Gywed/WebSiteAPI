using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Administrator.Employe;

public class UseCaseUpdateEmploye
{
    private IUserRepository _userRepository;

    public UseCaseUpdateEmploye(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Execute(DbUser input)
    {
        return _userRepository.Update(input);
    }
}