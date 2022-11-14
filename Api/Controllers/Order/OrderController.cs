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

    public OrderController(UseCaseConsultOrderContent useCaseConsultOrderContent, UseCaseConsultOrderOnDate useCaseConsultOrderOnDate)
    {
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
        _useCaseConsultOrderOnDate = useCaseConsultOrderOnDate;
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
    public ActionResult<IEnumerable<DtoOutputOrder>> FetchTodayOrder(DateTime date)
    {
        return  Ok(_useCaseConsultOrderOnDate.Execute(date));
    }
}