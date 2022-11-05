using Application.UseCases.Guest.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Employe;

public class UseCaseCreateEmploye: IUseCaseWriter<DtoOutputUser, DtoInputCreateUser>
{
    
    private readonly IUserRepository _userRepository;

    public UseCaseCreateEmploye(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public DtoOutputUser Execute(DtoInputCreateUser input)
    {
        var dbUser = _userRepository.Create(input.Surname, input.Lastname, input.Email, input.Age, input.Password, 1);

        return Mapper.GetInstance().Map<DtoOutputUser>(dbUser);
    }
    
}