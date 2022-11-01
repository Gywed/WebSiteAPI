using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Guest.Dtos;

public class DtoInputLogUser
{
    [Required] public string Email { get; set; }
    
    [Required] public string Password { get; set; }
}