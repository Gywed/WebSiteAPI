using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Guest.Dtos;

public class DtoInputCreateUser
{
    [Required] public string Surname { get; set; }
    
    [Required] public string Lastname { get; set; }
    
    [Required] public string Email { get; set; }
    
    [Required] public DateTime BirthDate { get; set; }
    
    [Required] public string Password { get; set; }
}