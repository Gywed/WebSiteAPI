namespace Infrastructure.Ef.DbEntities;

public class DbOrderHistoryContent
{
    public decimal Quantity { get; set; }
    
    public int IdOrder { get; set; }
    
    public int IdArticle { get; set; }
}