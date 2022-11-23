using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Article;

public class UseCaseCreateArticle: IUseCaseWriter<DtoOutputArticle, DtoInputCreateArticle>
{
    private readonly IArticleRepository _articleRepository;

    public UseCaseCreateArticle(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public DtoOutputArticle Execute(DtoInputCreateArticle input)
    {
        var dbArticle = _articleRepository.Create(input.Nametag, input.Price, input.Pricingtype, input.Stock, input.IdCategory, input.IdBrand);

        return Mapper.GetInstance().Map<DtoOutputArticle>(dbArticle);
    }
}