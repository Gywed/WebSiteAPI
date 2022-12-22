using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Application.Services.Article;
using Application.UseCases.Administrator.Article.Dtos;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Microsoft.IdentityModel.Tokens;

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
        // Give the place holder image if no path
        if (input.ImagePath == "")
            input.ImagePath = "assets/articles/No-Image-Placeholder.jpg";
        
        var dbArticle = _articleRepository.Create(input.Nametag, input.Price, input.Pricingtype
            , input.Stock, input.IdCategory, input.IdBrand, input.ImagePath);

        // If it's a place holder image don't try to save a new image
        if (dbArticle.ImagePath != "assets/articles/No-Image-Placeholder.jpg")
        {
            byte[] imageBytes = Convert.FromBase64String(input.ImageData);
            using MemoryStream memoryStream = new MemoryStream(imageBytes);
            var image = Image.FromStream(memoryStream);
        
            image.Save(input.ImagePath,ImageFormat.Jpeg);
        }
        
        return Mapper.GetInstance().Map<DtoOutputArticle>(_articleService.FetchById(dbArticle.Id));
    }
}