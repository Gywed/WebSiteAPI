using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IArticleRepository
{
    DbArticle FetchById(int id);
    IEnumerable<DbArticle> FetchAll();

    IEnumerable<DbArticle> FetchByName(string name);
    IEnumerable<DbArticle> FetchByCategoryId(int idCategory);
}