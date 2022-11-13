using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Employe;

public class UseCaseConsultOrderContent : IUseCaseParameterizedQuery<IEnumerable<DtoOutputOrder>,DtoInputOrder>
{
    private readonly IOrderRepository _orderRepository;

    public UseCaseConsultOrderContent(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public IEnumerable<DtoOutputOrder> Execute(DtoInputOrder dtoInput)
    {
        
        var order = _orderRepository.FetchById(dtoInput.Id);
        
        var ordersContents = order.
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputOrder>>(orders);
    }
}