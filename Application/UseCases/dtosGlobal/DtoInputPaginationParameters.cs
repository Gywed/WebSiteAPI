namespace Application.UseCases.dtosGlobal;

public class DtoInputPaginationParameters
{
    //number of the page in the frontEnd
    public int? NbPage { get; set; }
    //number of element shown on the page
    public int? NbElementsByPage { get; set; }
}