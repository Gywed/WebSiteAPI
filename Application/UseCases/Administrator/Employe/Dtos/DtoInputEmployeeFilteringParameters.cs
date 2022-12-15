using Application.UseCases.dtosGlobal;

namespace Application.UseCases.Administrator.Dtos;

public class DtoInputEmployeeFilteringParameters
{
    public string? Surname { get; set; }
    public string? Lastname { get; set; }

    public DtoInputPaginationParameters DtoPagination { get; set; }
}