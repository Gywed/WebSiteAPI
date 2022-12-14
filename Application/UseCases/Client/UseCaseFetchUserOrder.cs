using Application.UseCases.Employe;
using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseFetchUserOrder:IUseCaseParameterizedQuery<IEnumerable<DtoOutputOrder>,int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;

    public UseCaseFetchUserOrder(IOrderRepository orderRepository, UseCaseConsultOrderContent useCaseConsultOrderContent)
    {
        _orderRepository = orderRepository;
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
    }

    public IEnumerable<DtoOutputOrder> Execute(int idUser)
    {
        var userOrders = _orderRepository.FetchAllByUserId(idUser);


        var dtos = userOrders.Select(userOrder => 
            _useCaseConsultOrderContent.Execute(Mapper.GetInstance().Map<DtoInputOrder>(userOrder))).ToList();

        return dtos;
    }
}