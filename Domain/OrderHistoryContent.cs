namespace Domain;

public class OrderHistoryContent
{
    public int Id { get; set; }

    public Article Article { get; set; }
    
    public decimal Quantity { get; set; }
}