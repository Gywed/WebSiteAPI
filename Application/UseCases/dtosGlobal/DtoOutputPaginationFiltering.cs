namespace Application.UseCases.dtosGlobal;

public class DtoOutputPaginationFiltering<T>
{
    public IEnumerable<T> pageElements { get; set; }
    public int nbOfPages { get; set; }
}