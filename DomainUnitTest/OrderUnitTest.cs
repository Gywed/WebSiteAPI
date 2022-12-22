using Domain;
using DomainUnitTest.Factories;

namespace DomainUnitTest;

[TestFixture]
public class OrderUnitTest
{

    [Test]
    public void TotalOrderPrice_WithSomeOrderContent_ReturnExpectedTotal()
    {
        var order = new Order();
        var orderContent1 = OrderContentFactory.CreatePrice15Quantity2IsPreparedFalse();
        var orderContent2 = OrderContentFactory.CreatePrice920Quantity1IsPreparedTrue();
        
        order.Add(orderContent1);
        order.Add(orderContent2);
        
        Assert.That(order.TotalOrderPrice(), Is.EqualTo(39.2));
    }

    [Test]
    public void IsFullyPrepared_WithSomeOrderContent_ReturnFalse()
    {
        var order = new Order();
        var orderContent1 = OrderContentFactory.CreatePrice15Quantity2IsPreparedFalse();
        var orderContent2 = OrderContentFactory.CreatePrice920Quantity1IsPreparedTrue();
        
        order.Add(orderContent1);
        order.Add(orderContent2);
        
        Assert.That(order.IsFullyPrepared(), Is.False);
    }

    [Test]
    public void Of_WithEnumerableOfOrderContents_ReturnOrder()
    {
        var order = new Order();
        var orderContent1 = OrderContentFactory.CreatePrice15Quantity2IsPreparedFalse();
        var orderContent2 = OrderContentFactory.CreatePrice920Quantity1IsPreparedTrue();
        
        order.Add(orderContent1);
        order.Add(orderContent2);

        var orderContents = new List<OrderContent>().AsEnumerable();
        orderContents = orderContents.Append(orderContent1);
        orderContents = orderContents.Append(orderContent2);


        var orderToTest = Order.Of(orderContents);
        
        Assert.That(orderToTest.OrderContentItems(), Is.EqualTo(order.OrderContentItems()));
    }
    
}