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
        
        var order = _orderRepository.CreateOrders(dto.TakeDateTime,dto.IdUser);
        foreach (var orderContent in dto.DtosOrderContents)
        {
            var dbOrderContent = _orderRepository
                .CreateOrderContent(orderContent.Quantity,order.Id, orderContent.IdArticle, orderContent.Prepared);

        }
        return _useCaseConsultOrderContent.Execute(new DtoInputOrder
        {
            Id = order.Id,
            IdUser = order.IdUser
        });
    }
    
}