using Application.Services.Brand;
using Application.Services.Category;
using Infrastructure.Ef;

namespace Application.Services.Article;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;

    public ArticleService(IArticleRepository articleRepository, ICategoryService categoryService, IBrandService brandService)
    {
        _articleRepository = articleRepository;
        _categoryService = categoryService;
        _brandService = brandService;
    }

    public Domain.Article FetchById(int articleId)
    {
        var dbArticle = _articleRepository.FetchById(articleId);
        var article = new Domain.Article
        {
            Id = dbArticle.Id,
            Nametag = dbArticle.Nametag,
            Price = dbArticle.Price,
            Stock = dbArticle.Stock,
            PricingType = dbArticle.PricingType,
            brand = _brandService.FetchById(dbArticle.IdBrand),
            category = _categoryService.FetchById(dbArticle.IdCategory)
        };

        return article;
    }
}