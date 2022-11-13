using Application.Services.Order;
using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Employe;

public class UseCaseConsultOrderContent : IUseCaseParameterizedQuery<IEnumerable<DtoOutputOrder>,DtoInputOrder>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;

    public UseCaseConsultOrderContent(IOrderRepository orderRepository, IOrderService orderService)
    {
        _orderRepository = orderRepository;
        _orderService = orderService;
    }

    public IEnumerable<DtoOutputOrder> Execute(DtoInputOrder dtoInput)
    {
        var order = _orderService.Fetch(Mapper.GetInstance().Map<DbOrders>(dtoInput));

        var ordersContents = order.Where(oC => oC.Id == dtoInput.Id)
            .Entries();

        var dtos = new DtoOutputOrder
        {
            Date = order.Date,
            Id = order.Id,
            OrderContents = Mapper.GetInstance().Map<IEnumerable<DtoOutputOrder.OrderContent>>(ordersContents),
            TotalOrderPrice = order.TotalOrderPrice()
        };
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputOrder>>(dtos);
    }
}