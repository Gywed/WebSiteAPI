using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Administrator.Dtos;

public class DtoInputDeleteArticle
{
    [Required] public int Id { get; set; }
}