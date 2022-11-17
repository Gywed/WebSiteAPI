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

    public OrderController(UseCaseConsultOrderContent useCaseConsultOrderContent, UseCaseConsultOrderOnDate useCaseConsultOrderOnDate, UseCaseConsultOrderByUser useCaseConsultOrderByUser)
    {
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
        _useCaseConsultOrderOnDate = useCaseConsultOrderOnDate;
        _useCaseConsultOrderByUser = useCaseConsultOrderByUser;
    }

    [HttpGet]
    [Route("content")]
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
    [Route("{date:datetime}")]
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
    [Route("{name}")]
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
}