using Domain;
using DomainUnitTest.Factories;

namespace DomainUnitTest;

[TestFixture]
public class OrderUnitTest
{

    [Test]
    public void Add_WithOneOrderContent_ReturnListSizeEqualOne()
    {
        var orderContent = new OrderContent();
        var order = new Order();
        order.Add(orderContent);
        Assert.That(order.OrderContentItems().Count(), Is.EqualTo(1));
    }
    
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

    
    
}