using Application.UseCases.Client.Dtos;
using Application.UseCases.Employe;
using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseCreateOrderContent:IUseCaseWriter<DtoOutputOrder,DtoInputCreateOrder>
{
    private readonly IOrderRepository _orderRepository;
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;

    public UseCaseCreateOrderContent(IOrderRepository orderRepository, UseCaseConsultOrderContent useCaseConsultOrderContent)
    {
        _orderRepository = orderRepository;
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
    }

    public DtoOutputOrder Execute(DtoInputCreateOrder dto)
    {
        
        var order = _orderRepository.CreateOrders(dto.takedatetime,dto.userid);
        foreach (var orderContent in dto.DtosOrderContents)
        {
            var dbOrderContent = _orderRepository
                .CreateOrderContent(orderContent.quantity,order.Id, orderContent.idarticle, orderContent.prepared);

        }
        return _useCaseConsultOrderContent.Execute(new DtoInputOrder
        {
            Id = order.Id,
            IdUser = order.IdUser
        });
    }
    
}