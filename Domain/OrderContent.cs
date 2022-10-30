namespace Domain;

public class OrderContent
{
    public int Id { get; set; }
    
    public Order Order { get; set; }
    
    public Article Article { get; set; }
}