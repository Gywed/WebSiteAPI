namespace Application.Services.Article;

public interface IArticleService
{
    Domain.Article FetchById(int articleId);
}