using Application.UseCases.Administrator.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Administrator.Family;
using Application.UseCases.Client;
using Infrastructure.Ef.DbEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Article;

[ApiController]
[Route("api/v1/articles")]
public class ArticleController : ControllerBase
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
    private readonly UseCaseAddArticleInFamily _useCaseAddArticleInFamily;
    private readonly UseCaseFetchArticlesOfFamily _useCaseFetchArticlesOfFamily;
    private readonly UseCaseRemoveArticleFromFamily _useCaseRemoveArticleFromFamily;
    private readonly UseCaseFetchFamiliesOfArticle _useCaseFetchFamiliesOfArticle;
    private readonly UseCaseFetchArticlesInSameFamilies _useCaseFetchArticlesInSameFamilies;
    private readonly UseCaseFetchAllArticleFileName _useCaseFetchAllArticleFileName;
    

    public ArticleController(UseCaseFetchAllArticle useCaseFetchAllArticle, UseCaseSearchArticle useCaseSearchArticle,
        UseCaseCreateArticle useCaseCreateArticle
        , UseCaseDeleteArticle useCaseDeleteArticle, UseCaseUpdateArticle useCaseUpdateArticle,
        UseCaseFetchArticleById useCaseFetchArticleById, UseCaseCreateFamily useCaseCreateFamily,
        UseCaseDeleteFamily useCaseDeleteFamily, UseCaseUpdateFamily useCaseUpdateFamily,
        UseCaseFetchFamilies useCaseFetchFamilies, UseCaseAddArticleInFamily useCaseAddArticleInFamily,
        UseCaseFetchArticlesOfFamily useCaseFetchArticlesOfFamily,
        UseCaseRemoveArticleFromFamily useCaseRemoveArticleFromFamily, UseCaseFetchFamiliesOfArticle useCaseFetchFamiliesOfArticle, UseCaseFetchArticlesInSameFamilies useCaseFetchArticlesInSameFamilies, UseCaseFetchAllArticleFileName useCaseFetchAllArticleFileName)
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
        _useCaseAddArticleInFamily = useCaseAddArticleInFamily;
        _useCaseFetchArticlesOfFamily = useCaseFetchArticlesOfFamily;
        _useCaseRemoveArticleFromFamily = useCaseRemoveArticleFromFamily;
        _useCaseFetchFamiliesOfArticle = useCaseFetchFamiliesOfArticle;
        _useCaseFetchArticlesInSameFamilies = useCaseFetchArticlesInSameFamilies;
        _useCaseFetchAllArticleFileName = useCaseFetchAllArticleFileName;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchAll()
    {
        return Ok(_useCaseFetchAllArticle.Execute());
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<DtoOutputArticle> FetchById(int id)
    {
        return Ok(_useCaseFetchArticleById.Execute(id));
    }

    [HttpGet]
    [Route("{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchTodayOrder(string name)
    {
        return Ok(_useCaseSearchArticle.Execute(name));
    }

    [HttpPost]
//    [Authorize(Roles = "admin")]
    [Route("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<DtoOutputArticle> CreateArticle(DtoInputCreateArticle dto)
    {
        try
        {
            return Ok(_useCaseCreateArticle.Execute(dto));
        }
        catch (ArgumentException e)
        {
            return UnprocessableEntity(e.StackTrace);
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Delete(DtoInputDeleteArticle dto)
    {
        return _useCaseDeleteArticle.Execute(dto.Id) ? Ok() : NotFound();
    }

    [HttpPut]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> Update(DtoInputUpdateArticle dto)
    {
        return _useCaseUpdateArticle.Execute(dto) ? Ok() : NotFound();
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

    [HttpDelete]
    [Route("families/delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DtoOutputFamily> DeleteFamily(DtoInputDeleteFamily dto)
    {
        try
        {
            return Ok(_useCaseDeleteFamily.Execute(dto));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
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

    [HttpPost]
    [Route("families/addArticle")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<DtoOutputArticleFamily> AddArticleInFamily(DtoInputArticleFamily dto)
    {
        try
        {
            return Ok(_useCaseAddArticleInFamily.Execute(dto));
        }
        catch (ArgumentException e)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("families/removeArticle")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<bool> RemoveArticleFromFamily(DtoInputArticleFamily dto)
    {
        return _useCaseRemoveArticleFromFamily.Execute(dto) ? NoContent() : NotFound();
    }

    [HttpGet]
    [Route("families/{idFamily:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchArticlesOfFamily(int idFamily)
    {
        try
        {
            return Ok(_useCaseFetchArticlesOfFamily.Execute(idFamily));
        }
        catch (KeyNotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
    }
    
    [HttpGet]
    [Route("families/article/{idArticle:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputFamily>> FetchFamiliesOfArticle(int idArticle)
    {
        try
        {
            return Ok(_useCaseFetchFamiliesOfArticle.Execute(idArticle));
        }
        catch (KeyNotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
    }

    [HttpGet]
    [Route("families/articlesInSameFamilies/{idArticle:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<DtoOutputArticle>> FetchArticlesInSameFamilies(int idArticle)
    {
        try
        {
            return Ok(_useCaseFetchArticlesInSameFamilies.Execute(idArticle));
        }
        catch (KeyNotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
    }

    [HttpGet]
    [Route("articleFileName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string[]> FetchAllArticleFileName()
    {
        return Ok(_useCaseFetchAllArticleFileName.Execute());
    }
}