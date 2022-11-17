using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Microsoft.IdentityModel.Tokens;

namespace Application.UseCases.Employe;

public class UseCaseConsultOrderByUser : IUseCaseParameterizedQuery<IEnumerable<DtoOutputOrder>,string>
{
    private readonly IOrderRepository _orderRepository;
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;

    public UseCaseConsultOrderByUser(IOrderRepository orderRepository, UseCaseConsultOrderContent useCaseConsultOrderContent)
    {
        _orderRepository = orderRepository;
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
    }

    public IEnumerable<DtoOutputOrder> Execute(string name)
    {
        var todayOrders = _orderRepository.FetchAllByUserName(name);

        var dtos = new List<DtoOutputOrder>();

        foreach (var dbOrder in todayOrders)
        {
            dtos?.Add(_useCaseConsultOrderContent.Execute(Mapper.GetInstance().Map<DtoInputOrder>(dbOrder)));
        }

        if (dtos.IsNullOrEmpty())
            throw new ArgumentException($"There is no order for any user with the name {name}");
        
        return dtos;
    }
}