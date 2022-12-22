using Domain;
using DomainUnitTest.Factories;

namespace DomainUnitTest;

[TestFixture]
public class OrderHistoryUnitTest
{
    [Test]
    public void TotalOrderPrice_WithSomeOrderContent_ReturnExpectedTotal()
    {
        var order = new OrderHistory();
        var orderContent1 = OrderHistoryContentFactory.CreatePrice15Quantity2();
        var orderContent2 = OrderHistoryContentFactory.CreatePrice920Quantity1();
        
        order.Add(orderContent1);
        order.Add(orderContent2);
        
        Assert.That(order.TotalOrderPrice(), Is.EqualTo(39.2));
    }

}