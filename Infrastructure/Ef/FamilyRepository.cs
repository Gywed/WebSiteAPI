using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        var dbFamily = context.Families.FirstOrDefault(f => f.FamilyName == familyName);
        if (dbFamily != null)
            throw new ArgumentException($"This family name is already used");

        var newFamily = new DbFamily
        {
            FamilyName = familyName
        };

        context.Families.Add(newFamily);
        context.SaveChanges();
        return newFamily;
    }

    public DbFamily Delete(int id)
    {
        using var context = _contextProvider.NewContext();

        var family = context.Families.FirstOrDefault(f => f.Id == id);
        
        if (family == null)
            throw new KeyNotFoundException($"No family with the id {id}");
        
        var articlesInFamily = context.ArticleFamilies.Where(af => af.IdFamily == id);

        context.ArticleFamilies.RemoveRange(articlesInFamily);
        context.Families.Remove(family);
        context.SaveChanges();

        return family;
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
        var dbFamily = context.Families.FirstOrDefault(f => f.Id == idFamily);
        var dbArticleFamilies =
            context.ArticleFamilies.FirstOrDefault(af => af.IdArticle == idArticle && af.IdFamily == idFamily);
        
        if (dbArticle == null)
            throw new ArgumentException($"Article with Id {idArticle} doesn't exist");
        
        if (dbFamily == null)
            throw new ArgumentException($"Family with Id {idFamily} doesn't exist");
        
        if (dbArticleFamilies != null)
            throw new ArgumentException($"The article with Id {idArticle} is already in family with Id {idFamily}");
        
        
        
        var newArticleFamilies = new DbArticleFamilies
        {
            IdArticle = idArticle,
            IdFamily = idFamily
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
            context.ArticleFamilies.Remove(new DbArticleFamilies() { IdArticle = idArticle, IdFamily = idFamily});
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
        var dbFamily = context.Families.FirstOrDefault(f => f.Id == idFamily);
        if (dbFamily == null)
            throw new KeyNotFoundException($"Family with Id {idFamily} doesn't exist");
        
        var dbArticles = context.ArticleFamilies
            .Where(af=> af.IdFamily == idFamily)
            .Join(context.Articles
                , af =>af.IdArticle
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

    public IEnumerable<DbFamily> FetchFamiliesOfArticle(int idArticle)
    {
        using var context = _contextProvider.NewContext();
        var dbArticle = context.Articles.FirstOrDefault(a => a.Id == idArticle);
        if (dbArticle == null)
            throw new KeyNotFoundException($"Article with Id {idArticle} doesn't exist");
        
        var dbFamilies = context.ArticleFamilies
            .Where(af=> af.IdArticle == idArticle)
            .Join(context.Families
                , af =>af.IdFamily
                , f => f.Id
                , (af, f) => new DbFamily
                {
                    Id = f.Id,
                    FamilyName = f.FamilyName
                })
            .ToList();
        return dbFamilies;
    }
}