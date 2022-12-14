using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class FamilyRepository: IFamilyRepository
{
    private readonly TakeAndDashContextProvider _contextProvider;

    public FamilyRepository(TakeAndDashContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public DbFamily Create(string familyName)
    {
        using var context = _contextProvider.NewContext();

        var dbFamily = context.Families.FirstOrDefault(f => f.family_name == familyName);
        if (dbFamily != null)
            throw new ArgumentException($"This family name is already used");

        var newFamily = new DbFamily
        {
            family_name = familyName
        };

        context.Families.Add(newFamily);
        context.SaveChanges();
        return newFamily;
    }

    public bool Delete(int id)
    {
        using var context = _contextProvider.NewContext();

        var articlesInFamily = context.ArticleFamilies.Where(af => af.id_family == id);
        
        try
        {
            context.ArticleFamilies.RemoveRange(articlesInFamily);
            context.Families.Remove(new DbFamily{id=id});
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public bool Update(DbFamily family)
    {
        using var context = _contextProvider.NewContext();
        try
        {
            context.Families.Update(family);
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public IEnumerable<DbFamily> FetchAll()
    {
        using var context = _contextProvider.NewContext();
        return context.Families.ToList();
    }

    public DbArticleFamilies AddArticleInFamily(int idArticle, int idFamily)
    {
        using var context = _contextProvider.NewContext();
        var dbArticle = context.Articles.FirstOrDefault(a => a.Id == idArticle);
        var dbFamily = context.Families.FirstOrDefault(f => f.id == idFamily);
        var dbArticleFamilies =
            context.ArticleFamilies.FirstOrDefault(af => af.id_article == idArticle && af.id_family == idFamily);
        
        if (dbArticle == null)
            throw new ArgumentException($"Article with id {idArticle} doesn't exist");
        
        if (dbFamily == null)
            throw new ArgumentException($"Family with id {idFamily} doesn't exist");
        
        if (dbArticleFamilies != null)
            throw new ArgumentException($"The article with id {idArticle} is already in family with id {idFamily}");
        
        
        
        var newArticleFamilies = new DbArticleFamilies
        {
            id_article = idArticle,
            id_family = idFamily
        };
        context.ArticleFamilies.Add(newArticleFamilies);
        context.SaveChanges();
        return newArticleFamilies;

    }

    public bool RemoveArticleFromFamily(int idArticle, int idFamily)
    {
        using var context = _contextProvider.NewContext();
        try
        {
            context.ArticleFamilies.Remove(new DbArticleFamilies() { id_article = idArticle, id_family = idFamily});
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }

    public IEnumerable<DbArticle> FetchArticlesOfFamily(int idFamily)
    {
        using var context = _contextProvider.NewContext();
        var dbFamily = context.Families.FirstOrDefault(f => f.id == idFamily);
        if (dbFamily == null)
            throw new KeyNotFoundException($"Family with id {idFamily} doesn't exist");
        
        var dbArticles = context.ArticleFamilies
            .Where(af=> af.id_family == idFamily)
            .Join(context.Articles
                , af =>af.id_article
                , a => a.Id
                , (af, a) => new DbArticle
                {
                    Id = a.Id,
                    Nametag = a.Nametag,
                    Price = a.Price,
                    Stock = a.Stock,
                    IdBrand = a.IdBrand,
                    IdCategory = a.IdCategory,
                    PricingType = a.PricingType 
                })
            .ToList();
        return dbArticles;
    }
}