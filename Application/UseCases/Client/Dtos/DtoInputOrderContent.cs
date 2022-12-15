namespace Application.UseCases.Client.Dtos;

public class DtoInputOrderContent
{
    public int IdArticle { get; set; }
    public decimal Quantity { get; set; }

    // Default value for first created order (always start unprepared) 
    public bool Prepared { get; set; } = false;
}