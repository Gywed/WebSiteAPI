namespace Infrastructure.Ef.DbEntities;

public class DbArticle
{
    public int id { get; set; }
    
    public string nametage { get; set; }
    
    public double price { get; set; }
    
    public int pricingtype { get; set; }
    
    public int stock { get; set; }
    
    public int idcategory { get; set; }
}