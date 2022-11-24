using Application.UseCases.dtosGlobal;

namespace Application.UseCases.Administrator.Dtos;

public class DtoInputEmployeeFilteringParameters
{
    public string? surname { get; set; }
    public string? lastname { get; set; }

    public DtoInputPaginationParameters dtoPagination { get; set; }
}