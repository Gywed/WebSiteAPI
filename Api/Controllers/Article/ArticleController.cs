using Application.UseCases.Administrator.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Client;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Article;

[ApiController]
[Route("api/v1/articles")]
public class ArticleController:ControllerBase
{
    private readonly UseCaseFetchAllArticle _useCaseFetchAllArticle;
    private readonly UseCaseSearchArticle _useCaseSearchArticle;
    private readonly UseCaseCreateArticle _useCaseCreateArticle;
    private readonly UseCaseDeleteArticle _useCaseDeleteArticle;

    public ArticleController(UseCaseFetchAllArticle useCaseFetchAllArticle, UseCaseSearchArticle useCaseSearchArticle, UseCaseCreateArticle useCaseCreateArticle
        , UseCaseDeleteArticle useCaseDeleteArticle)
    {
        _useCaseFetchAllArticle = useCaseFetchAllArticle;
        _useCaseSearchArticle = useCaseSearchArticle;
        _useCaseCreateArticle = useCaseCreateArticle;
        _useCaseDeleteArticle = useCaseDeleteArticle;
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
    
    [HttpPost]
    [Route("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<DtoOutputArticle> CreateArticle(DtoInputCreateArticle dto)
    {
        try
        {
            var output = _useCaseCreateArticle.Execute(dto);
            return output;
        }
        catch (ArgumentException e)
        {
            return UnprocessableEntity(new
            {
                e.Message
            });
        }
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(DtoInputDeleteArticle dto)
    {
        return _useCaseDeleteArticle.Execute(dto.Id)? Ok() : NotFound();
    }
}