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

    public Domain.Order FetchOrder(DbOrders order)
    {
        var dbOrder = _orderRepository.FetchOrderById(order.Id);
        var dbOrderContents = _orderRepository.FetchContentByOrder(dbOrder);
        var orderContents = dbOrderContents.Select(dbOrderContent => new OrderContent
        {
            Article = _articleService.FetchById(dbOrderContent.IdArticle),
            Id = dbOrderContent.IdOrder,
            Quantity = dbOrderContent.Quantity,
            Prepared = dbOrderContent.Prepared 
        });
        
        var domOrder = Domain.Order.Of(orderContents);
        domOrder.Id = dbOrder.Id;
        domOrder.TakeDateTime = dbOrder.TakeDateTime;
        domOrder.CreationDate = dbOrder.CreationDate;

        return domOrder;
    }

    public OrderHistory FetchOrderHistory(DbOrdersHistory order)
    {
        var dbOrder = _orderRepository.FetchOrderHistoryById(order.Id);
        var dbOrderContents = _orderRepository.FetchContentByOrderHistory(dbOrder);
        var orderContents = dbOrderContents.Select(dbOrderContent => new OrderHistoryContent
        {
            Article = _articleService.FetchById(dbOrderContent.IdArticle),
            Id = dbOrderContent.IdOrder,
            Quantity = dbOrderContent.Quantity,
        });
        
        var domOrder = OrderHistory.Of(orderContents);
        domOrder.Id = dbOrder.Id;
        domOrder.TakenDateTime = dbOrder.TakenDateTime;
        domOrder.CreationDate = dbOrder.CreationDate;

        return domOrder;
    }
}