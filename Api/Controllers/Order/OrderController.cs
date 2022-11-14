using Application.UseCases.Employe;
using Application.UseCases.Employe.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Order;

[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;
    private readonly UseCaseConsultTodayOrder _useCaseConsultTodayOrder;

    public OrderController(UseCaseConsultOrderContent useCaseConsultOrderContent, UseCaseConsultTodayOrder useCaseConsultTodayOrder)
    {
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
        _useCaseConsultTodayOrder = useCaseConsultTodayOrder;
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
    [Route("today")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<DtoOutputOrder>> FetchTodayOrder()
    {
        return  Ok(_useCaseConsultTodayOrder.Execute());
    }
}