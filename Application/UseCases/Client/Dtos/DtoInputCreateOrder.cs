using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Client.Dtos;

public class DtoInputCreateOrder
{
    public DateTime TakeDateTime { get; set; }
    public int IdUser { get; set; }
    public List<DtoInputOrderContent> DtosOrderContents { get; set; }
}