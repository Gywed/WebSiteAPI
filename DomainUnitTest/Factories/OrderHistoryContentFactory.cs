using Domain;

namespace DomainUnitTest.Factories;

public class OrderHistoryContentFactory
{
    public static OrderHistoryContent CreatePrice15Quantity2()
    {
        return new OrderHistoryContent()
        {
            Article = new Article
            {
                Price = 15
            },
            Quantity = 2
        };
    }
    
    public static OrderHistoryContent CreatePrice920Quantity1()
    {
        return new OrderHistoryContent
        {
            Article = new Article
            {
                Price = (decimal)9.20
            },
            Quantity = 1
        };
    }
}