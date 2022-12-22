using Application.Services.Order;
using Application.UseCases.dtosGlobal.DtoEntities;
using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Employe;

public class UseCaseConsultOrderContent : IUseCaseParameterizedQuery<DtoOutputOrder,DtoInputOrder>
{
    private readonly IOrderService _orderService;

    public UseCaseConsultOrderContent(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public DtoOutputOrder Execute(DtoInputOrder dtoInput)
    {
        var order = _orderService.FetchOrder(Mapper.GetInstance().Map<DbOrders>(dtoInput));

        var dtos = new DtoOutputOrder
        {
            CreationDate = order.CreationDate,
            TakeDateTime = order.TakeDateTime,
            Id = dtoInput.Id,
            OrderContents = Mapper.GetInstance().Map<List<DtoOutputOrder.OrderContent>>(order.Entries()),
            TotalOrderPrice = order.TotalOrderPrice(),
            IsFullyPrepared = order.IsFullyPrepared()
        };
        return Mapper.GetInstance().Map<DtoOutputOrder>(dtos);
    }
}