using System.Data.SqlTypes;

namespace Infrastructure.Ef.DbEntities;

public class DbOrderContent
{
    public decimal Quantity { get; set; }
    
    public int IdOrder { get; set; }
    
    public int IdArticle { get; set; }
    
    public bool Prepared { get; set; }
}