using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Administrator.Employe.Dtos;
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
        user.Surname = input.Surname;
        user.Lastname = input.Lastname;
        user.Birthdate = input.BirthDate;
        user.Permission = input.Permission;
        
        return _userRepository.Update(user);
    }
}