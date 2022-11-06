using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Employe;

public class UseCaseConsultOrdersOfTheDay : IUserCaseQuery<IEnumerable<DtoOutputOrder>>
{
    private readonly IOrderRepository _orderRepository;

    public UseCaseConsultOrdersOfTheDay(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public IEnumerable<DtoOutputOrder> Execute()
    {
        var orders = _orderRepository.FetchContentByOrders(DateTime.Today.ToString("dd/MM/yyyy"));
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputOrder>>(orders);
    }
}