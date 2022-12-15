using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class ArticleRepository : IArticleRepository
{
    private readonly TakeAndDashContextProvider _contextProvider;

    public ArticleRepository(TakeAndDashContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public DbArticle FetchById(int id)
    {
        using var context = _contextProvider.NewContext();
        var article = context.Articles.FirstOrDefault(a => a.Id == id);
        
        if (article == null)
            throw new KeyNotFoundException($"Article with Id {id} has not been found");

        return article;
    }

    public IEnumerable<DbArticle> FetchAll()
    {
        using var context = _contextProvider.NewContext();
        return context.Articles.ToList();
    }

    public IEnumerable<DbArticle> FetchByName(string name)
    {
        using var context = _contextProvider.NewContext();
        return context.Articles.Where(a => a.Nametag.Contains(name)).ToList();
        
    }

    public IEnumerable<DbArticle> FetchByCategoryId(int idCategory)
    {
        using var context = _contextProvider.NewContext();
        return context.Articles.Where(a => a.IdCategory == idCategory).ToList();
    }
    
    public DbArticle Create(string nametag, decimal price, int pricingtype, int stock, int idCategory, int idBrand)
    {
        using var context = _contextProvider.NewContext();

        var article = new DbArticle
        {
            Nametag = nametag,
            Price = price,
            PricingType = pricingtype,
            Stock = stock,
            IdCategory = idCategory,
            IdBrand = idBrand
        };
        context.Articles.Add(article);
        context.SaveChanges();
        return article;
    }
    
    public bool Delete(int id)
    {
        using var context = _contextProvider.NewContext();
        try
        {
            context.Articles.Remove(new DbArticle() { Id = id });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }
    
    public bool Update(DbArticle article)
    {
        using var context = _contextProvider.NewContext();

        try
        {
            context.Articles.Update(article);
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }        
    }
}