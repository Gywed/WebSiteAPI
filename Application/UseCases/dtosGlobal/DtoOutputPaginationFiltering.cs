namespace Application.UseCases.dtosGlobal;

public class DtoOutputPaginationFiltering<T>
{
    public IEnumerable<T> PageElements { get; set; }
    public int NbOfPages { get; set; }
}