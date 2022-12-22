using System.ComponentModel.DataAnnotations;
using Application.UseCases.Client.Dtos;

namespace Application.UseCases.Administrator.Dtos;

public class DtoInputUpdateArticle
{
    [Required] public int Id { get; set; }
    
    [Required] public string Nametag { get; set; }
    
    [Required] public decimal Price { get; set; }
    
    [Required] public int Pricingtype { get; set; }
    
    [Required] public int Stock { get; set; }
    
    [Required] public DtoOutputCategory Category { get; set; }
    
    [Required] public DtoOutputBrands Brand { get; set; }
    
    [Required] public string ImagePath { get; set; }
}