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


    public DtoOutputUser Execute(DtoInputCreateUser dto)
    {
        var dbUser = _userRepository.Create(dto.Surname, dto.Lastname, dto.Email, dto.BirthDate, dto.Password, 0);

        return Mapper.GetInstance().Map<DtoOutputUser>(dbUser);
    }
}