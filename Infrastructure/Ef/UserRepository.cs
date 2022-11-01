using System.Security.Cryptography;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Ef;

public class UserRepository : IUserRepository
{
    private readonly TakeAndDashContextProvider _contextProvider;

    public UserRepository(TakeAndDashContextProvider contextProvider)
    {
        _contextProvider = contextProvider;
    }

    public IEnumerable<DbUser> FetchAll()
    {
        using var context = _contextProvider.NewContext();
        return context.Users.ToList();
    }

    public DbUser FetchById(int id)
    {
        using var context = _contextProvider.NewContext();
        var user = context.Users.FirstOrDefault(u => u.id == id);

        if (user == null) throw new KeyNotFoundException($"User with id {id} has not been found");

        return user;
    }

    public DbUser FetchByCredential(string email, string password)
    {
        using var context = _contextProvider.NewContext();
        
        var user = context.Users.FirstOrDefault(u => u.email == email);
        
        if (user == null) throw new KeyNotFoundException($"User with this email and password has not been found");

        string hsh = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: Convert.FromBase64String(user!.salt),
            prf:KeyDerivationPrf.HMACSHA256,
            iterationCount:100000,
            numBytesRequested:256/8
        ));
        
        if (hsh != user.hsh) throw new KeyNotFoundException($"User with this email and password has not been found");

        return user;
    }

    public DbUser Create(string surname, string lastName, string email, int age, string password)
    {
        using var context = _contextProvider.NewContext();

        
        // Transform the password to hashed password
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

        string hsh = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf:KeyDerivationPrf.HMACSHA256,
            iterationCount:100000,
            numBytesRequested:256/8
            ));
        
        var user = new DbUser
        {
            surname = surname,
            lastname = lastName,
            email = email,
            age = age,
            hsh = hsh,
            permission = 0,
            salt = Convert.ToBase64String(salt)
        };
        context.Users.Add(user);
        context.SaveChanges();
        return user;
    }
}