using Application.UseCases.Guest.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Guest;

public class UseCaseFetchUsernameByEmail:IUseCaseParameterizedQuery<DtoOutputUsername,string>
{
    private readonly IUserRepository _userRepository;

    public UseCaseFetchUsernameByEmail(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public DtoOutputUsername Execute(string email)
    {
        var dbUser = _userRepository.FetchUsernameByEmail(email);
        
        return Mapper.GetInstance().Map<DtoOutputUsername>(dbUser);
    }
}