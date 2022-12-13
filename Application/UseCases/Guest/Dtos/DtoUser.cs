namespace Application.UseCases.Guest.Dtos;

public class DtoUser
{
    public string Email { get; set; }
    public string Role { get; set; }
    
    public int ?Id { get; set; }
}