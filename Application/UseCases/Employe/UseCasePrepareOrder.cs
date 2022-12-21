using Application.Services.Order;
using Application.UseCases.dtosGlobal.DtoEntities;
using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Employe;

public class UseCasePrepareOrder : IUseCaseParameterizedQuery<DtoOutputOrderHistory,DtoInputOrder>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;

    public UseCasePrepareOrder(IOrderService orderService, IOrderRepository orderRepository)
    {
        _orderService = orderService;
        _orderRepository = orderRepository;
    }
    
    public DtoOutputOrderHistory Execute(DtoInputOrder dto)
    {
        var dbOrder = _orderRepository.FetchOrderById(dto.Id);
        
        var order = _orderService.FetchOrder(dbOrder);

        if (!order.IsFullyPrepared())
            throw new ArgumentException($"This order isn't fully prepared");

        var dbOrderHistory = _orderRepository.CreateOrdersHistory(new DbOrdersHistory
        {
            CreationDate = order.CreationDate,
            IdUser = dto.IdUser,
            TakenDateTime = DateTime.Now
        });

        foreach (var orderContent in order.Entries())
        {
            _orderRepository.CreateOrdersHistoryContent(new DbOrderHistoryContent
            {
                IdOrder = dbOrderHistory.Id,
                IdArticle = orderContent.Article.Id,
                Quantity = orderContent.Quantity
            });
        }

        return Mapper.GetInstance().Map<DtoOutputOrderHistory>(_orderService.FetchOrderHistory(dbOrderHistory));
    }
}