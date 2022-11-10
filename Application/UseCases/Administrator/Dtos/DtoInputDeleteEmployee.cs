using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Administrator.Dtos;

public class DtoInputDeleteEmployee
{
    [Required] public int Id { get; set; }
}