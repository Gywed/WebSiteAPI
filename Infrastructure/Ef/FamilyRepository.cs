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
        var dbFamily = context.Families.FirstOrDefault(f => f.id == id);
        if (dbFamily == null)
            throw new KeyNotFoundException($"Family with id {id} doesn't exist");

        var articlesInFamily = context.ArticleFamilies.Where(af => af.id_family == id);
        
        try
        {
            context.ArticleFamilies.RemoveRange(articlesInFamily);
            context.Families.Remove(dbFamily);
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }
}