using Application.UseCases.Client;
using Application.UseCases.Client.Dtos;
using Application.UseCases.dtosGlobal;
using Application.UseCases.dtosGlobal.DtoEntities;
using Application.UseCases.Employe;
using Application.UseCases.Employe.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Order;

[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;
    private readonly UseCaseConsultOrderOnDate _useCaseConsultOrderOnDate;
    private readonly UseCaseConsultOrderByUserName _useCaseConsultOrderByUserName;
    private readonly UseCaseConsultOrderByBothDateAndUser _useCaseConsultOrderByBothDateAndUser;
    private readonly UseCaseConsultOrderByCategory _useCaseConsultOrderByCategory;
    private readonly UseCaseCreateOrderContent _useCaseCreateOrderContent;
    private readonly UseCaseUpdatePreparedArticle _useCaseUpdatePreparedArticle;
    private readonly UseCaseConsultOrderByUserId _useCaseConsultOrderByUserId;

    public OrderController(UseCaseConsultOrderContent useCaseConsultOrderContent, UseCaseConsultOrderOnDate useCaseConsultOrderOnDate, UseCaseConsultOrderByUserName useCaseConsultOrderByUserName, UseCaseConsultOrderByBothDateAndUser useCaseConsultOrderByBothDateAndUser, UseCaseConsultOrderByCategory useCaseConsultOrderByCategory, UseCaseCreateOrderContent useCaseCreateOrderContent, UseCaseUpdatePreparedArticle useCaseUpdatePreparedArticle, UseCaseConsultOrderByUserId useCaseConsultOrderByUserId)
    {
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
        _useCaseConsultOrderOnDate = useCaseConsultOrderOnDate;
        _useCaseConsultOrderByUserName = useCaseConsultOrderByUserName;
        _useCaseConsultOrderByBothDateAndUser = useCaseConsultOrderByBothDateAndUser;
        _useCaseConsultOrderByCategory = useCaseConsultOrderByCategory;
        _useCaseCreateOrderContent = useCaseCreateOrderContent;
        _useCaseUpdatePreparedArticle = useCaseUpdatePreparedArticle;
        _useCaseConsultOrderByUserId = useCaseConsultOrderByUserId;
    }

    
    
    [HttpGet]
    [Route("content/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputOrder> FetchContentOrder(int id)
    {
        try
        {
            return Ok(_useCaseConsultOrderContent.Execute(new DtoInputOrder
            {
                Id = id,
                IdUser = 0
            }));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        
    }
    
    [HttpGet]
    [Route("date/{date:datetime}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputOrder>> FetchTodayOrder(DateTime date)
    {
        try
        {
            return  Ok(_useCaseConsultOrderOnDate.Execute(date));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet]
    [Route("user_name/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputOrder>> FetchOrderByUserName(string name)
    {
        try
        {
            return  Ok(_useCaseConsultOrderByUserName.Execute(name));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet]
    [Route("filter/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputOrder>> 
        FetchFilteredOrderByUserAndOrDate([FromQuery] string? name, [FromQuery] DateTime? date)
    {
        try
        {
            return Ok(_useCaseConsultOrderByBothDateAndUser.Execute(new DtoInputOrderFiltering
            {
                Name = name,
                Date = date
            }));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpGet]
    [Route("category/{idCategory:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputOrder>> FetchOrderByCategory(int idCategory)
    {
        try
        {
            return Ok(_useCaseConsultOrderByCategory.Execute(idCategory));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "client")]
    [Route("orderContent")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<DtoOutputOrder> CreateOrderContent(DtoInputCreateOrder dto)
    {
        dto.IdUser = int.Parse(User.Claims.First(i => i.Type == "Id").Value);
        return StatusCode(201,_useCaseCreateOrderContent.Execute(dto));

    }

    [HttpGet]
    [Authorize(Roles = "client,employe,admin")]
    [Route("client")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputOrder>> FetchOrderByUserId()
    {
        var userid = int.Parse(User.Claims.First(i => i.Type == "Id").Value);
        try
        {
            return Ok(_useCaseConsultOrderByUserId.Execute(userid));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPatch]
    [Route("orderContent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> UpdatePreparedOrderContent(DtoInputUpdateOrderContent dto)
    {
        try
        {
            return Ok(_useCaseUpdatePreparedArticle.Execute(dto));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}