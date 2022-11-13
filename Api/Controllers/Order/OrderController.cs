using Application.UseCases.Employe;
using Application.UseCases.Employe.Dtos;
using Infrastructure.Ef.DbEntities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Order;

[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;

    public OrderController(UseCaseConsultOrderContent useCaseConsultOrderContent)
    {
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
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
}