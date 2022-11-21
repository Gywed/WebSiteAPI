namespace Infrastructure.Ef.DbEntities;

public class DbOrdersHistory
{
    public int Id { get; set; }
    
    public DateTime TakeDateTime { get; set; }
    
    public DateTime CreationDate { get; set; }
    public int IdUser { get; set; }
}