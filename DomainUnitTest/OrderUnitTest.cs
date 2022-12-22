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

}