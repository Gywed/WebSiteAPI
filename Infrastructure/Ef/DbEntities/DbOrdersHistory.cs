namespace Infrastructure.Ef.DbEntities;

public class DbOrdersHistory
{
    public int Id { get; set; }
    public DateTime TakenDateTime { get; set; }
    public DateTime CreationDate { get; set; }
    public int IdUser { get; set; }
}