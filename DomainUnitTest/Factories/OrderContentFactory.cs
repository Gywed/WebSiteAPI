using Domain;

namespace DomainUnitTest.Factories;

public class OrderContentFactory
{
    public static OrderContent CreatePrice15Quantity2IsPreparedFalse()
    {
        return new OrderContent
        {
            Article = new Article
            {
                Price = 15
            },
            Quantity = 2,
            Prepared = false
        };
    }
    
    public static OrderContent CreatePrice920Quantity1IsPreparedTrue()
    {
        return new OrderContent
        {
            Article = new Article
            {
                Price = (decimal)9.20
            },
            Quantity = 1,
            Prepared = true
        };
    }
}