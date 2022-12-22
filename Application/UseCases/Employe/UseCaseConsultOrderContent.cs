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
        var order = _orderService.Fetch(Mapper.GetInstance().Map<DbOrders>(dtoInput));

        var ordersContents = order.Where(oC => oC.Id == dtoInput.Id)
            .OrderContentItems();

        var dtos = new DtoOutputOrder
        {
            CreationDate = order.CreationDate,
            TakeDateTime = order.TakeDateTime,
            Id = dtoInput.Id,
            OrderContents = Mapper.GetInstance().Map<List<DtoOutputOrder.OrderContent>>(ordersContents),
            TotalOrderPrice = order.TotalOrderPrice(),
            IsFullyPrepared = order.IsFullyPrepared()
        };
        return Mapper.GetInstance().Map<DtoOutputOrder>(dtos);
    }
}