using Application.UseCases.Administrator.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Client;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Article;

[ApiController]
[Route("api/v1/articles")]
public class ArticleController:ControllerBase
{
    private readonly UseCaseFetchAllArticle _useCaseFetchAllArticle;
    private readonly UseCaseSearchArticle _useCaseSearchArticle;

    public ArticleController(UseCaseFetchAllArticle useCaseFetchAllArticle, UseCaseSearchArticle useCaseSearchArticle)
    {
        _useCaseFetchAllArticle = useCaseFetchAllArticle;
        _useCaseSearchArticle = useCaseSearchArticle;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchAll()
    {
        return Ok(_useCaseFetchAllArticle.Execute());
    }
    
    [HttpGet]
    [Route("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchTodayOrder(string name)
    {
        return  Ok(_useCaseSearchArticle.Execute(name));
    }
}