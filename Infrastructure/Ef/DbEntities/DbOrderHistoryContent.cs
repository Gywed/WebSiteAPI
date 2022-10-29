namespace Infrastructure.Ef.DbEntities;

public class DbOrderHistoryContent
{
    public double quantity { get; set; }
    
    public int idorder { get; set; }
    
    public int idarticle { get; set; }
}