using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using Application.UseCases.Administrator.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Administrator.Article;

public class UseCaseUpdateArticle:IUseCaseParameterizedQuery<bool,DtoInputUpdateArticle>
{
    private IArticleRepository _articleRepository;
    private readonly UseCaseFetchAllArticleFileName _useCaseFetchAllArticleFileName;
    private bool hasNewPath;


    public UseCaseUpdateArticle(IArticleRepository articleRepository, UseCaseFetchAllArticleFileName useCaseFetchAllArticleFileName)
    {
        _articleRepository = articleRepository;
        _useCaseFetchAllArticleFileName = useCaseFetchAllArticleFileName;
        hasNewPath = false;
    }

    public bool Execute(DtoInputUpdateArticle dto)
    {
        var dbArticle = _articleRepository.FetchById(dto.Id);
        
        // If no new image keep old path
        if (dto.ImagePath == "")
        {
            dto.ImagePath = dbArticle.ImagePath;
        }
        
        // Give a new path if this is a new image
        if (dto.ImagePath == "new")
        {
            hasNewPath = true;
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
            dto.ImagePath = newPath;
        }
        
        if (hasNewPath)
        {
            if (dbArticle.ImagePath != "assets/articles/No-Image-Placeholder.jpg")
                File.Delete(dbArticle.ImagePath);
            
            byte[] imageBytes = Convert.FromBase64String(dto.ImageData);
            using MemoryStream memoryStream = new MemoryStream(imageBytes);
            var image = Image.FromStream(memoryStream);
        
            image.Save(dto.ImagePath,ImageFormat.Jpeg);
        }
        
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