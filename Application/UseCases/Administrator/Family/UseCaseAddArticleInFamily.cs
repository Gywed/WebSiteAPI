using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Family;

public class UseCaseAddArticleInFamily:IUseCaseWriter<DtoOutputArticleFamily, DtoInputArticleFamily>
{
    private readonly IFamilyRepository _familyRepository;

    public UseCaseAddArticleInFamily(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public DtoOutputArticleFamily Execute(DtoInputArticleFamily dto)
    {
        var dbArticleFamily = _familyRepository.AddArticleInFamily(dto.IdArticle, dto.IdFamily);
        return Mapper.GetInstance().Map<DtoOutputArticleFamily>(dbArticleFamily);
    }
}