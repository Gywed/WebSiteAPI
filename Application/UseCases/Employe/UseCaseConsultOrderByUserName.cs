using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Microsoft.IdentityModel.Tokens;

namespace Application.UseCases.Employe;

public class UseCaseConsultOrderByUserName : IUseCaseParameterizedQuery<IEnumerable<DtoOutputOrder>,string>
{
    private readonly IOrderRepository _orderRepository;
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;

    public UseCaseConsultOrderByUserName(IOrderRepository orderRepository, UseCaseConsultOrderContent useCaseConsultOrderContent)
    {
        _orderRepository = orderRepository;
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
    }

    public IEnumerable<DtoOutputOrder> Execute(string name)
    {
        var userOrders = _orderRepository.FetchAllByUserName(name);

        var dtos = userOrders.Select(dbOrder => _useCaseConsultOrderContent.Execute(Mapper.GetInstance().Map<DtoInputOrder>(dbOrder))).ToList();

        if (dtos.IsNullOrEmpty())
            throw new ArgumentException($"There is no order for any user with the name {name}");
        
        return dtos;
    }
}