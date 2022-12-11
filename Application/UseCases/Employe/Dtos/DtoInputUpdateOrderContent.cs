namespace Application.UseCases.Employe.Dtos;

public class DtoInputUpdateOrderContent
{
    public int orderid { get; set; }
    public int articleid { get; set; }
    public bool prepared { get; set; }
}