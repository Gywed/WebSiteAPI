using Application.UseCases.Client;
using Application.UseCases.Client.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Category;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController:ControllerBase
{
    private readonly UseCaseFetchAllCategories _useCaseFetchAllCategories;

    public CategoryController(UseCaseFetchAllCategories useCaseFetchAllCategories)
    {
        _useCaseFetchAllCategories = useCaseFetchAllCategories;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputCategory>> FetchAll()
    {
        return Ok(_useCaseFetchAllCategories.Execute());
    }
}