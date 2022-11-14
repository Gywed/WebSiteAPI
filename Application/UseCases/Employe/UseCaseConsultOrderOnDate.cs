using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Microsoft.IdentityModel.Tokens;

namespace Application.UseCases.Employe;

public class UseCaseConsultOrderOnDate : IUseCaseParameterizedQuery<IEnumerable<DtoOutputOrder>,DateTime>
{
    private readonly IOrderRepository _orderRepository;
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;

    public UseCaseConsultOrderOnDate(IOrderRepository orderRepository, UseCaseConsultOrderContent useCaseConsultOrderContent)
    {
        _orderRepository = orderRepository;
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
    }

    public IEnumerable<DtoOutputOrder> Execute(DateTime date)
    {
        var todayOrders = _orderRepository.FetchAllByDate(date.Date);

        var dtos = new List<DtoOutputOrder>();

        foreach (var dbOrder in todayOrders)
        {
            dtos?.Add(_useCaseConsultOrderContent.Execute(Mapper.GetInstance().Map<DtoInputOrder>(dbOrder)));
        }

        if (dtos.IsNullOrEmpty())
            throw new ArgumentException($"There is no order on the {date}");
        
        return dtos;
    }
}