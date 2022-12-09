using Application.UseCases.Client;
using Application.UseCases.Client.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTakeAndDash.Controllers.Brand;

[ApiController]
[Route("api/v1/brands")]
public class BrandController: ControllerBase
{
    private readonly UseCaseFetchAllBrands _useCaseFetchAllBrands;

    public BrandController(UseCaseFetchAllBrands useCaseFetchAllBrands)
    {
        _useCaseFetchAllBrands = useCaseFetchAllBrands;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DtoOutputBrands>> FetchAll()
    {
        return Ok(_useCaseFetchAllBrands.Execute());
    }
}