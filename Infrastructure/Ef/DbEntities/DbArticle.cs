namespace Infrastructure.Ef.DbEntities;

public class DbArticle
{
    public int Id { get; set; }
    
    public string Nametag { get; set; }
    
    public decimal Price { get; set; }
    
    public int PricingType { get; set; }
    
    public int Stock { get; set; }
    
    public int IdCategory { get; set; }
    public int IdBrand { get; set; }
    public string ImagePath { get; set; }
}