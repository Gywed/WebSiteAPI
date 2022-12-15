namespace Infrastructure.Ef.DbEntities;

public class DbUser
{
    public int Id { get; set; }
    
    public string Lastname { get; set; }
    
    public string Surname { get; set; }
    
    public string Email { get; set; }
    
    public DateTime Birthdate { get; set; }
    
    public int Permission { get; set; }
    
    public string Hsh { get; set; }
    
    public string Salt { get; set; }
}