using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Client.Dtos;

public class DtoInputCreateOrder
{
    public DateTime takedatetime { get; set; }
    public int userid { get; set; }
    public List<DtoInputOrderContent> DtosOrderContents { get; set; }
}