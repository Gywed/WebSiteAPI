using Application.Services;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Administrator.Employe;
using Application.UseCases.Guest;
using Application.UseCases.Guest.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
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
    private readonly UseCaseCreateEmploye _useCaseCreateEmploye;
    private readonly UseCaseFetchAllEmploye _useCaseFetchAllEmploye;
    private readonly UseCaseDeleteEmploye _useCaseDeleteEmploye;
    private readonly UseCaseFetchPaginationEmployee _useCaseFetchPaginationEmployee;

    public UserController(UseCaseSignUp useCaseSignUp, IConfiguration configuration, TokenService tokenService, UseCaseLogIn useCaseLogIn, UseCaseCreateEmploye useCaseCreateEmploye, UseCaseFetchAllEmploye useCaseFetchAllEmploye, UseCaseDeleteEmploye useCaseDeleteEmploye, UseCaseFetchPaginationEmployee useCaseFetchPaginationEmployee)
    {
        _useCaseSignUp = useCaseSignUp;
        _configuration = configuration;
        _tokenService = tokenService;
        _useCaseLogIn = useCaseLogIn;
        _useCaseCreateEmploye = useCaseCreateEmploye;
        _useCaseFetchAllEmploye = useCaseFetchAllEmploye;
        _useCaseDeleteEmploye = useCaseDeleteEmploye;
        _useCaseFetchPaginationEmployee = useCaseFetchPaginationEmployee;
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
    
    [HttpPost]
    [Route("employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<DtoOutputUser> CreateEmploye(DtoInputCreateUser dto)
    {
        try
        {
            var output = _useCaseCreateEmploye.Execute(dto);
            return output;
        }
        catch (ArgumentException e)
        {
            return UnprocessableEntity(new
            {
                e.Message
            });
        }
        
    }
    
    [HttpGet]
    [Route("employee")]
    public ActionResult<IEnumerable<DtoOutputUser>> FetchAllEmployee()
    {
        return Ok(_useCaseFetchAllEmploye.Execute());
    }
    
    [HttpGet]
    [Route("employee/test")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<IEnumerable<DtoOutputUser>> FetchPaginationEmployee([FromQuery] int? nbPage, [FromQuery] int? nbElementsByPage)
    {
        try
        {
            return Ok(_useCaseFetchPaginationEmployee.Execute(new DtoInputPaginationFilteringParameters
            {
                nbPage = nbPage,
                nbElementsByPage = nbElementsByPage
            }));
        }
        catch (ArgumentException e)
        {
            return StatusCode(422, e.Message);
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(DtoInputDeleteEmployee dto)
    {
        return _useCaseDeleteEmploye.Execute(dto.Id)? Ok() : NotFound();
    }
}