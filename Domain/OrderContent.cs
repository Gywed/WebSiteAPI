namespace Domain;

public class OrderContent
{
    public int Id { get; set; }

    public Article Article { get; set; }
    
    public double Quantity { get; set; }
}