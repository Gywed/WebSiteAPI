using Application.UseCases.Guest.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Guest;

public class UseCaseLogIn : IUseCaseWriter<DtoUser,DtoInputLogUser>
{   
    private readonly IUserRepository _userRepository;

    public UseCaseLogIn(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public DtoUser Execute(DtoInputLogUser dto)
    {
        var dbUser = _userRepository.FetchByCredential(dto.Email, dto.Password);

        DtoUser dtoUser = new DtoUser
        {
            Email = dto.Email,
            Role = dbUser.permission == 0 ? "client" : dbUser.permission == 1 ? "employe" : "administrator"
        };

        return dtoUser;
    }
}