using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Employe;

public class UseCaseConsultOrderByCategory : IUseCaseParameterizedQuery<IEnumerable<DtoOutputOrder>, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly UseCaseConsultOrderContent _useCaseConsultOrderContent;

    public UseCaseConsultOrderByCategory(IOrderRepository orderRepository, UseCaseConsultOrderContent useCaseConsultOrderContent)
    {
        _orderRepository = orderRepository;
        _useCaseConsultOrderContent = useCaseConsultOrderContent;
    }

    public IEnumerable<DtoOutputOrder> Execute(int idCategory)
    {
        var categoryOrders = _orderRepository.FetchAllByCategoryId(idCategory);

        var dtos = categoryOrders.Select(categoryOrder =>
            _useCaseConsultOrderContent.Execute(Mapper.GetInstance().Map<DtoInputOrder>(categoryOrder))).ToList();

        return dtos;
    }
}