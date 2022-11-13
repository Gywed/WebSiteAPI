namespace Infrastructure.Ef.DbEntities;

public class DbOrders
{
    public int Id { get; set; }
    
    public string CreationDate { get; set; }
    
    public int IdUser { get; set; }
}