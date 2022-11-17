namespace Application.UseCases.Client.Dtos;

public class DtoOutputArticle
{
    public int Id { get; set; }
    public string Nametag { get; set; }
    public decimal Price { get; set; }
    public int PricingType { get; set; }
    public int Stock { get; set; }
    public int IdCategory { get; set; }
}