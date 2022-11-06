namespace Domain;

public class Order
{
    private readonly List<OrderContent> _entries = new();

    public int Id { get; set; }
    public string Date { get; set; }

    public static Order Of(IEnumerable<OrderContent> orderContents)
    {
        var order = new Order();
        order.AddRange(orderContents);
        return order;
    }
    
    public void Add(OrderContent orderContent)
    {
        _entries.Add(orderContent);
    }

    public void AddRange(IEnumerable<OrderContent> contents)
    {
        foreach (var orderContent in contents)
            Add(orderContent);
    }

    public double TotalOrderPrice()
    {
        return _entries.Sum(o => o.Article.Price * o.Quantity);
    }

    public Order Where(Predicate<OrderContent> predicate)
    {
        return Of(_entries.Where(predicate.Invoke));
    }
    public IEnumerable<OrderContent> Entries()
    {
        return _entries;
    }

}