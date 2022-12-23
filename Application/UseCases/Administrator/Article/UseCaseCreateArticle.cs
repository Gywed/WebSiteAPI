using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
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
    private readonly UseCaseFetchAllArticleFileName _useCaseFetchAllArticleFileName;

    public UseCaseCreateArticle(IArticleRepository articleRepository, IArticleService articleService
        , UseCaseFetchAllArticleFileName useCaseFetchAllArticleFileName)
    {
        _articleRepository = articleRepository;
        _articleService = articleService;
        _useCaseFetchAllArticleFileName = useCaseFetchAllArticleFileName;
    }

    public DtoOutputArticle Execute(DtoInputCreateArticle input)
    {
        // Give the place holder image if no path
        if (input.ImagePath == "")
            input.ImagePath = "assets/articles/No-Image-Placeholder.jpg";

        // Give a new path if this is a new image
        if (input.ImagePath == "new")
        {
            var filenames = _useCaseFetchAllArticleFileName.Execute();
            var suitablePath = false;
            var newPath = "";
            while (!suitablePath)
            {
                var randomGenerator = RandomNumberGenerator.Create();
                const int min = 501;
                const int max = 9999999;

                // Enough space to go up to 2147483647 (3 bytes who was 16777215 doesn't work for some reason)
                var bytes = new byte[4];
                randomGenerator.GetBytes(bytes);

                // Convert To integer
                var randomNumber = Math.Abs(BitConverter.ToInt32(bytes, 0));

                // Check range
                randomNumber = randomNumber % (max - min + 1) + min;
                newPath = $"assets/articles/asset-{randomNumber}.jpg";
                
                // Check if the path already exist
                suitablePath = true;
                foreach (var name in filenames)
                {
                    if (newPath == name)
                        suitablePath = false;
                }
            }
            input.ImagePath = newPath;
        }
        
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