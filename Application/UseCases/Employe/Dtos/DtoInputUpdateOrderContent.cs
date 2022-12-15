namespace Application.UseCases.Employe.Dtos;

public class DtoInputUpdateOrderContent
{
    public int IdOrder { get; set; }
    public int IdArticle { get; set; }
    public bool Prepared { get; set; }
}