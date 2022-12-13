namespace Application.UseCases.Guest.Dtos;

public class DtoOutputUser
{
    public int Id { get; set; }
    
    public string Surname { get; set; }
    
    public string Lastname { get; set; }
    
    public string Email { get; set; }

    public DateTime Birthdate { get; set; }

    public int permission { get; set; }
}