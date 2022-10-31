using Application.UseCases.Guest;
using Application.UseCases.Guest.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.User;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly UseCaseSignUp _useCaseSignUp;

    public UserController(UseCaseSignUp useCaseSignUp)
    {
        _useCaseSignUp = useCaseSignUp;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputUser> Create(DtoInputCreateUser dto)
    {
        var output = _useCaseSignUp.Execute(dto);

        return output;
    }
}