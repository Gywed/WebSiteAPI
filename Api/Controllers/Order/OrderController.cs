using Application.UseCases.dtosGlobal;
using Application.UseCases.Employe;
using Application.UseCases.Employe.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Order;

[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;
    private readonly UseCaseConsultOrderOnDate _useCaseConsultOrderOnDate;
    private readonly UseCaseConsultOrderByUser _useCaseConsultOrderByUser;
    private readonly UseCaseConsultOrderByBothDateAndUser _useCaseConsultOrderByBothDateAndUser;
    private readonly UseCaseConsultOrderByCategory _useCaseConsultOrderByCategory;

    public OrderController(UseCaseConsultOrderContent useCaseConsultOrderContent, UseCaseConsultOrderOnDate useCaseConsultOrderOnDate, UseCaseConsultOrderByUser useCaseConsultOrderByUser, UseCaseConsultOrderByBothDateAndUser useCaseConsultOrderByBothDateAndUser, UseCaseConsultOrderByCategory useCaseConsultOrderByCategory)
    {
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
        _useCaseConsultOrderOnDate = useCaseConsultOrderOnDate;
        _useCaseConsultOrderByUser = useCaseConsultOrderByUser;
        _useCaseConsultOrderByBothDateAndUser = useCaseConsultOrderByBothDateAndUser;
        _useCaseConsultOrderByCategory = useCaseConsultOrderByCategory;
    }

    
    
    [HttpGet]
    [Route("content/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<DtoOutputOrder> FetchContentOrder(int id)
    {
        return  Ok(_useCaseConsultOrderContent.Execute(new DtoInputOrder
        {
            Id = id,
            IdUser = 0
        }));
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
            return  Ok(_useCaseConsultOrderByUser.Execute(name));
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
                name = name,
                date = date
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
    public ActionResult<IEnumerable<DtoOutputOrder>> 
        FetchOrderByCategory(int idCategory)
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
}