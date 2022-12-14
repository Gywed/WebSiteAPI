using Application.UseCases.Administrator.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Administrator.Family;
using Application.UseCases.Client;
using Infrastructure.Ef.DbEntities;
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
    private readonly UseCaseUpdateArticle _useCaseUpdateArticle;
    private readonly UseCaseFetchArticleById _useCaseFetchArticleById;
    private readonly UseCaseCreateFamily _useCaseCreateFamily;
    private readonly UseCaseDeleteFamily _useCaseDeleteFamily;
    private readonly UseCaseUpdateFamily _useCaseUpdateFamily;
    private readonly UseCaseFetchFamilies _useCaseFetchFamilies;

    public ArticleController(UseCaseFetchAllArticle useCaseFetchAllArticle, UseCaseSearchArticle useCaseSearchArticle, UseCaseCreateArticle useCaseCreateArticle
        , UseCaseDeleteArticle useCaseDeleteArticle, UseCaseUpdateArticle useCaseUpdateArticle, UseCaseFetchArticleById useCaseFetchArticleById, UseCaseCreateFamily useCaseCreateFamily, UseCaseDeleteFamily useCaseDeleteFamily, UseCaseUpdateFamily useCaseUpdateFamily, UseCaseFetchFamilies useCaseFetchFamilies)
    {
        _useCaseFetchAllArticle = useCaseFetchAllArticle;
        _useCaseSearchArticle = useCaseSearchArticle;
        _useCaseCreateArticle = useCaseCreateArticle;
        _useCaseDeleteArticle = useCaseDeleteArticle;
        _useCaseUpdateArticle = useCaseUpdateArticle;
        _useCaseFetchArticleById = useCaseFetchArticleById;
        _useCaseCreateFamily = useCaseCreateFamily;
        _useCaseDeleteFamily = useCaseDeleteFamily;
        _useCaseUpdateFamily = useCaseUpdateFamily;
        _useCaseFetchFamilies = useCaseFetchFamilies;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchAll()
    {
        return Ok(_useCaseFetchAllArticle.Execute());
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchById(int id)
    {
        return Ok(_useCaseFetchArticleById.Execute(id));
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
    
    [HttpPut]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> Update(DbArticle dto)
    {
        return _useCaseUpdateArticle.Execute(dto)? Ok() : NotFound();
    }
    
    //FAMILIES
    //###########################
    [HttpPost]
    [Route("families/create")]
    [ProducesResponseType((StatusCodes.Status200OK))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<DtoOutputFamily> CreateFamily(DtoInputCreateFamily dto)
    {
        try
        {
            return Ok(_useCaseCreateFamily.Execute(dto));
        }
        catch (ArgumentException e)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity, e.Message);
        }
    }

    [HttpPost]
    [Route("families/delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> DeleteFamily(DtoInputDeleteFamily dto)
    {
        try
        {
            return Ok(_useCaseDeleteFamily.Execute(dto));
        }
        catch (KeyNotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
    }

    [HttpPut]
    [Route("families/update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> UpdateFamily(DtoInputUpdateFamily dto)
    {
        return _useCaseUpdateFamily.Execute(dto) ? NoContent() : NotFound();
    }

    [HttpGet]
    [Route("families")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<DtoOutputFamily>> FetchFamilies()
    {
        return Ok(_useCaseFetchFamilies.Execute());
    }
}