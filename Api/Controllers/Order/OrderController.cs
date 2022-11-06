using Application.UseCases.Employe;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Order;

[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly UseCaseConsultOrdersOfTheDay _useCaseConsultOrdersOfTheDay;

    public OrderController(UseCaseConsultOrdersOfTheDay useCaseConsultOrdersOfTheDay)
    {
        _useCaseConsultOrdersOfTheDay = useCaseConsultOrdersOfTheDay;
    }
}