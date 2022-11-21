namespace Application.UseCases.Utils;

public interface IUseCaseWriter<out TOutput,in TInput>
{
    TOutput Execute(TInput dto);
}