namespace Infrastructure.Ef.DbEntities;

public class DbOrderContent
{
    public decimal quantity { get; set; }
    
    public int idorder { get; set; }
    
    public int idarticle { get; set; }
}