namespace Application.Services.Category;

public interface ICategoryService
{
    Domain.Category FetchById(int categoryId);
}