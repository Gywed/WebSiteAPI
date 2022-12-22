using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Administrator.Article;

public class UseCaseUpdateArticle:IUseCaseParameterizedQuery<bool,DtoInputUpdateArticle>
{
    private IArticleRepository _articleRepository;

    public UseCaseUpdateArticle(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public bool Execute(DtoInputUpdateArticle dto)
    {
        return _articleRepository.Update(new DbArticle
        {
            Id = dto.Id,
            Nametag = dto.Nametag,
            Price = dto.Price,
            Stock = dto.Stock,
            IdBrand = dto.Brand.Id,
            PricingType = dto.Pricingtype,
            IdCategory = dto.Category.Id,
            ImagePath = dto.ImagePath
        });
    }
}