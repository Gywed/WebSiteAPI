using Application.UseCases.Client.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Client;

public class UseCaseFetchAllCategories:
    IUseCaseQuery<IEnumerable<DtoOutputCategory>>
{
    private readonly ICategoryRepository _categoryRepository;

    public UseCaseFetchAllCategories(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<DtoOutputCategory> Execute()
    {
        var categories = _categoryRepository.FetchAll();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputCategory>>(categories);
    }
}