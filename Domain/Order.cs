namespace Domain;

public class Order
{
    private readonly List<OrderContent> _orderContentItems = new();

    public int Id { get; set; }
    public DateTime TakeDateTime { get; set; }
    public DateTime CreationDate { get; set; }

    public static Order Of(IEnumerable<OrderContent> orderContents)
    {
        var order = new Order();
        order.AddRange(orderContents);
        return order;
    }
    
    public void Add(OrderContent orderContent)
    {
        _orderContentItems.Add(orderContent);
    }

    public void AddRange(IEnumerable<OrderContent> contents)
    {
        foreach (var orderContent in contents)
            Add(orderContent);
    }

    public decimal TotalOrderPrice()
    {
        return _orderContentItems.Sum(o => o.Article.Price * o.Quantity);
    }

    public bool IsFullyPrepared()
    {
        return _orderContentItems.TrueForAll(content => content.Prepared);
    }
    public IEnumerable<OrderContent> OrderContentItems()
    {
        return _orderContentItems;
    }

}
