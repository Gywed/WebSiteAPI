using Application.Services.Order;
using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Microsoft.IdentityModel.Tokens;

namespace Application.UseCases.Employe;

public class UseCaseConsultTodayOrder : IUseCaseQuery<IEnumerable<DtoOutputOrder>>
{
    private readonly IOrderService _orderService;
    private readonly IOrderRepository _orderRepository;
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;

    public UseCaseConsultTodayOrder(IOrderService orderService, IOrderRepository orderRepository, UseCaseConsultOrderContent useCaseConsultOrderContent)
    {
        _orderService = orderService;
        _orderRepository = orderRepository;
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
    }

    public IEnumerable<DtoOutputOrder> Execute()
    {
        var todayOrders = _orderRepository.FetchAllByDate(DateTime.Today.Date);

        var dtos = new List<DtoOutputOrder>();

        foreach (var dbOrder in todayOrders)
        {
            dtos?.Add(_useCaseConsultOrderContent.Execute(Mapper.GetInstance().Map<DtoInputOrder>(dbOrder)));
        }

        if (dtos.IsNullOrEmpty())
            throw new ArgumentException($"There is no order today");
        
        return dtos;
    }
}