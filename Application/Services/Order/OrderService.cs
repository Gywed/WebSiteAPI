using Application.Services.Article;
using Domain;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.Services.Order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IArticleService _articleService;

    public OrderService(IOrderRepository orderRepository, IArticleService articleService)
    {
        _orderRepository = orderRepository;
        _articleService = articleService;
    }

    public Domain.Order Fetch(DbOrders order)
    {
        var dbOrder = _orderRepository.FetchById(order.Id);
        var dbOrderContents = _orderRepository.FetchContentByOrder(dbOrder);
        var orderContents = dbOrderContents.Select(dbOrderContent => new OrderContent
        {
            Article = _articleService.FetchById(dbOrderContent.idarticle),
            Id = dbOrderContent.idorder,
            Quantity = dbOrderContent.quantity
        });
        
        var domOrder = Domain.Order.Of(orderContents);
        domOrder.Id = dbOrder.Id;
        domOrder.Date = dbOrder.CreationDate;

        return domOrder;
    }
}