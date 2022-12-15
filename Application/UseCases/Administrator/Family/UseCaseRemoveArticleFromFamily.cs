using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Family;

public class UseCaseRemoveArticleFromFamily:IUseCaseWriter<bool, DtoInputArticleFamily>
{
    private readonly IFamilyRepository _familyRepository;

    public UseCaseRemoveArticleFromFamily(IFamilyRepository familyRepository)
    {
        _familyRepository = familyRepository;
    }

    public bool Execute(DtoInputArticleFamily dto)
    {
        return _familyRepository.RemoveArticleFromFamily(dto.IdArticle, dto.IdFamily);
    }
}