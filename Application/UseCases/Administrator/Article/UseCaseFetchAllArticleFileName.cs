using Application.UseCases.Utils;

namespace Application.UseCases.Administrator.Article;

public class UseCaseFetchAllArticleFileName : IUseCaseQuery<string[]>
{
    public string[] Execute()
    {
        const string directoryPath = "assets/articles";
        var fileNames = Directory.GetFiles(directoryPath);
        for (var i = 0; i < fileNames.Length; i++)
        {
            fileNames[i] = fileNames[i].Replace("\\", "/");
        }

        return fileNames;
    }
}