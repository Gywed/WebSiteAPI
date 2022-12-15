using Application.UseCases.Employe.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Microsoft.VisualBasic.CompilerServices;

namespace Application.UseCases.Employe;

public class UseCaseUpdatePreparedArticle : IUseCaseParameterizedQuery<bool,DtoInputUpdateOrderContent>
{
    private readonly IOrderRepository _orderRepository;

    public UseCaseUpdatePreparedArticle(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public bool Execute(DtoInputUpdateOrderContent dto)
    {
        return _orderRepository.UpdateOrderContentPrepared(dto.IdOrder, dto.IdArticle, dto.Prepared);
    }
}