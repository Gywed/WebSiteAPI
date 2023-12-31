using Application.UseCases.Employe.Dtos;

namespace Application.UseCases.Utils;

public interface IUseCaseParameterizedQuery<out TOutput, in TParam>
{
    TOutput Execute(TParam param);
}
