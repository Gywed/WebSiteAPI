using Application.UseCases.Employe.Dtos;

namespace Application.UseCases.Utils;

public interface IUseCaseParameterizedQuery<out TOutput, in TParam>
{
    IEnumerable<DtoOutputOrder> Execute(TParam param);
}