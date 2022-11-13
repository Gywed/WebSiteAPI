namespace Infrastructure.Ef.DbEntities;

public class DbArticle
{
    public int Id { get; set; }
    
    public string Nametag { get; set; }
    
    public double Price { get; set; }
    
    public int PricingType { get; set; }
    
    public int Stock { get; set; }
    
    public int IdCategory { get; set; }
}