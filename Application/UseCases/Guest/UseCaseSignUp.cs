using Application.UseCases.Guest.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Guest;

public class UseCaseSignUp : IUseCaseWriter<DtoOutputUser,DtoInputCreateUser>
{
    private readonly IUserRepository _userRepository;

    public UseCaseSignUp(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public DtoOutputUser Execute(DtoInputCreateUser input)
    {
        var dbUser = _userRepository.Create(input.Surname, input.Lastname, input.Email, input.Age, input.Password);

        return Mapper.GetInstance().Map<DtoOutputUser>(dbUser);
    }
}