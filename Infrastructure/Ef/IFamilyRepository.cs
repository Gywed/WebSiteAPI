using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IFamilyRepository
{
    DbFamily Create(string familyName);
    DbFamily Delete(int id);
    bool Update(DbFamily family);
    IEnumerable<DbFamily> FetchAll();
    DbArticleFamilies AddArticleInFamily(int idArticle, int idFamily);
    bool RemoveArticleFromFamily(int idArticle, int idFamily);
    IEnumerable<DbArticle> FetchArticlesOfFamily(int idFamily);
    IEnumerable<DbFamily> FetchFamiliesOfArticle(int idArticle);
    IEnumerable<DbArticle> FetchArticlesInSameFamilies(int idArticle);
}