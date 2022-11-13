namespace Application.UseCases.Utils;

public interface IUserCaseQuery<out TOutput>
{
    TOutput Execute();
}