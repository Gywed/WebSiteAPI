namespace Application.UseCases.Utils;

public interface IUseCaseWriter<TOutput, TInput>
{
    TOutput Execute(TInput input);
}