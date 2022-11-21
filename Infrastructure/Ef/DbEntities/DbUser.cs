namespace Infrastructure.Ef.DbEntities;

public class DbUser
{
    public int id { get; set; }
    
    public string lastname { get; set; }
    
    public string surname { get; set; }
    
    public string email { get; set; }
    
    public DateTime birthdate { get; set; }
    
    public int permission { get; set; }
    
    public string hsh { get; set; }
    
    public string salt { get; set; }
}