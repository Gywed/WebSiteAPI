using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Employe;

public class UseCaseCancelOrder:IUseCaseWriter<bool, DtoInputDeleteOrder>
{
    private readonly IOrderRepository _orderRepository;

    public UseCaseCancelOrder(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public bool Execute(DtoInputDeleteOrder dto)
    {
        return _orderRepository.DeleteOrder(dto.IdOrder);
        
    }
}