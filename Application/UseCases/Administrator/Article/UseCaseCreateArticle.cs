using Application.Services.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Administrator.Article;

public class UseCaseCreateArticle: IUseCaseWriter<DtoOutputArticle, DtoInputCreateArticle>
{
    private readonly IArticleService _articleService;
    private readonly IArticleRepository _articleRepository;

    public UseCaseCreateArticle(IArticleRepository articleRepository, IArticleService articleService)
    {
        _articleRepository = articleRepository;
        _articleService = articleService;
    }

    public DtoOutputArticle Execute(DtoInputCreateArticle input)
    {
        var dbArticle = _articleRepository.Create(input.Nametag, input.Price, input.Pricingtype
            , input.Stock, input.IdCategory, input.IdBrand, input.ImagePath);

        return Mapper.GetInstance().Map<DtoOutputArticle>(_articleService.FetchById(dbArticle.Id));
    }
}