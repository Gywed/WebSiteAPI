using Application.UseCases.Administrator.Dtos;
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

    public bool Execute(DtoInputUpdateUser input)
    {
        DbUser user = _userRepository.FetchById(input.Id);
        user.surname = input.Surname;
        user.lastname = input.Lastname;
        user.birthdate = input.BirthDate;
        user.permission = input.Permission;
        
        return _userRepository.Update(user);
    }
}