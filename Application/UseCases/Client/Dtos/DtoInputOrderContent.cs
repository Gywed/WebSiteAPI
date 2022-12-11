namespace Application.UseCases.Client.Dtos;

public class DtoInputOrderContent
{
    public int idarticle { get; set; }
    public decimal quantity { get; set; }

    // Default value for first created order (always start unprepared) 
    public bool prepared { get; set; } = false;
}