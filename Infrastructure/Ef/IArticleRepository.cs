using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IArticleRepository
{
    DbArticle FetchById(int id);
    IEnumerable<DbArticle> FetchAll();

    IEnumerable<DbArticle> FetchByName(string name);
    IEnumerable<DbArticle> FetchByCategoryId(int idCategory);
    
    IEnumerable<DbArticle> FetchByCategoryIdAndName(string name, int idCategory);
    
    DbArticle Create(string nametag, decimal price, int pricingtype, int stock, int idCategory, int idBrand, string imagePath);
    
    bool Delete(int id);
    
    bool Update(DbArticle article);
}