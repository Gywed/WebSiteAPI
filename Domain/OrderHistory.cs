namespace Domain;

public class OrderHistory
{
    private readonly List<OrderHistoryContent> _entries = new();

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
        _entries.Add(orderContent);
    }

    public void AddRange(IEnumerable<OrderHistoryContent> contents)
    {
        foreach (var orderContent in contents)
            Add(orderContent);
    }

    public decimal TotalOrderPrice()
    {
        return _entries.Sum(o => o.Article.Price * o.Quantity);
    }

    public OrderHistory Where(Predicate<OrderHistoryContent> predicate)
    {
        return Of(_entries.Where(predicate.Invoke));
    }
    public IEnumerable<OrderHistoryContent> Entries()
    {
        return _entries;
    }
}