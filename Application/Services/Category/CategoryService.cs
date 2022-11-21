using Infrastructure.Ef;

namespace Application.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Domain.Category FetchById(int categoryId)
    {
        var dbCategory = _categoryRepository.FetchById(categoryId);

        return Mapper.GetInstance().Map<Domain.Category>(dbCategory);
    }
}