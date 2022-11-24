namespace Application.UseCases.dtosGlobal;

public class DtoInputPaginationParameters
{
    //number of the page in the frontEnd
    public int? nbPage { get; set; }
    //number of element shown on the page
    public int? nbElementsByPage { get; set; }
}