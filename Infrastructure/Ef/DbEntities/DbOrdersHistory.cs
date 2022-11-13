namespace Infrastructure.Ef.DbEntities;

public class DbOrdersHistory
{
    public int id { get; set; }
    
    public DateTime creationDate { get; set; }
    
    public int iduser { get; set; }
}