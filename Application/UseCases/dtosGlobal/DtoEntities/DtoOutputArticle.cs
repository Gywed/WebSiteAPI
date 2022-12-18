using Application.UseCases.Client.Dtos;

namespace Application.UseCases.Administrator.Article.Dtos;

public class DtoOutputArticle
{
    public int Id { get; set; }
    public string Nametag { get; set; }
    public decimal Price { get; set; }
    public int PricingType { get; set; }
    public int Stock { get; set; }
    public DtoOutputCategory category { get; set; }
    public DtoOutputBrands brand { get; set; }
    public string imagePath { get; set; }
}