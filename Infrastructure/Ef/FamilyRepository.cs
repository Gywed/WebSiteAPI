using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

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
}