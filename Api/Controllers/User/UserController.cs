using Application.Services;
using Application.UseCases.Guest;
using Application.UseCases.Guest.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.User;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly IConfiguration _configuration;
    private string _generatedToken = "";
    
    
    private readonly UseCaseSignUp _useCaseSignUp;
    private readonly UseCaseLogIn _useCaseLogIn;

    public UserController(UseCaseSignUp useCaseSignUp, IConfiguration configuration, TokenService tokenService, UseCaseLogIn useCaseLogIn)
    {
        _useCaseSignUp = useCaseSignUp;
        _configuration = configuration;
        _tokenService = tokenService;
        _useCaseLogIn = useCaseLogIn;
    }
    
    [HttpPost]
    [Route("client")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<DtoOutputUser> Create(DtoInputCreateUser dto)
    {
        try
        {
            var output = _useCaseSignUp.Execute(dto);
            return output;
        }
        catch (ArgumentException e)
        {
            // Means that the request is understandable but we refuse to execute it
            // (here because the email is already used)
            return UnprocessableEntity(new
            {
                e.Message
            });
        }
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<string> Login(DtoInputLogUser dto)
    {

        try
        {
            _generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"], _configuration["Jwt:Issuer"], _useCaseLogIn.Execute(dto));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new
            {
                e.Message
            });
        }
        Response.Cookies.Append("Token", _generatedToken,new CookieOptions
        {
            Secure = true,
            HttpOnly = true
        });
        return Ok();
    }
}