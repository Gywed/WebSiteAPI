using Application.UseCases.Administrator.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Article;

[ApiController]
[Route("api/v1/articles")]
public class ArticleController:ControllerBase
{
    private readonly UseCaseFetchAllArticle _useCaseFetchAllArticle;

    public ArticleController(UseCaseFetchAllArticle useCaseFetchAllArticle)
    {
        _useCaseFetchAllArticle = useCaseFetchAllArticle;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchAll()
    {
        return Ok(_useCaseFetchAllArticle.Execute());
    }
}