using System.Collections;
using System.Security.Cryptography;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

    //Fecth a fixed amount of employee for the web pagination
    public IEnumerable<DbUser> FetchPaginationEmployee(int nbPage, int nbElementsByPage)
    {
        using var context = _contextProvider.NewContext();
        return context.Users.Skip((nbPage-1)*nbElementsByPage).Take(nbElementsByPage).ToList();
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

    public DbUser Create(string surname, string lastName, string email, int age, string password, int permission)
    {
        using var context = _contextProvider.NewContext();

        // Check if the email is already used or not
        var userDb = context.Users.FirstOrDefault(u => u.email == email);
        if (userDb != null)
            throw new ArgumentException($"This email is already used");
            
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
            permission = permission,
            salt = Convert.ToBase64String(salt)
        };
        context.Users.Add(user);
        context.SaveChanges();
        return user;
    }
    
    //Fetch all employees
    public IEnumerable<DbUser> FetchAllEmployees()
    {
        using var context = _contextProvider.NewContext();
        var employees =  context.Users.ToList().FindAll(u => u.permission == 1);

        return employees;

    }

    public bool Delete(int id)
    {
        using var context = _contextProvider.NewContext();
        try
        {
            context.Users.Remove(new DbUser { id = id });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }
}