using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Administrator.Dtos;

public class DtoInputCreateArticle
{
    [Required] public string Nametag { get; set; }
    
    [Required] public decimal Price { get; set; }
    
    [Required] public int Pricingtype { get; set; }
    
    [Required] public int Stock { get; set; }
    
    [Required] public int IdCategory { get; set; }
    
    [Required] public int IdBrand { get; set; }
    
    [Required] public string ImagePath { get; set; }
}