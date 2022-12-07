using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Administrator.Dtos;

public class DtoInputUpdateUser
{
    [Required] public int Id { get; set; }
    [Required] public string Surname { get; set; }
    
    [Required] public string Lastname { get; set; }
    
    [Required] public DateTime BirthDate { get; set; }
    
    [Required] public int Permission { get; set; }
}