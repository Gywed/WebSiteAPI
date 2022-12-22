namespace Domain;

public class OrderHistory
{
    private readonly List<OrderHistoryContent> _orderHistoryContentItems = new();

    public int Id { get; set; }
    public DateTime TakenDateTime { get; set; }
    public DateTime CreationDate { get; set; }

    public static OrderHistory Of(IEnumerable<OrderHistoryContent> orderContents)
    {
        var order = new OrderHistory();
        order.AddRange(orderContents);
        return order;
    }
    
    public void Add(OrderHistoryContent orderContent)
    {
        _orderHistoryContentItems.Add(orderContent);
    }

    public void AddRange(IEnumerable<OrderHistoryContent> contents)
    {
        foreach (var orderContent in contents)
            Add(orderContent);
    }

    public decimal TotalOrderPrice()
    {
        return _orderHistoryContentItems.Sum(o => o.Article.Price * o.Quantity);
    }
    
    public IEnumerable<OrderHistoryContent> OrderHistoryContentItems()
    {
        return _orderHistoryContentItems;
    }
}