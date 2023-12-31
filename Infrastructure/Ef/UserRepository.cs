using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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
    
    public IEnumerable<DbUser> FetchEmployeesFilteredPagination(int nbPage, int nbElementsByPage, string surname,
        string lastname)
    {
        using var context = _contextProvider.NewContext();
        return context.Users.Where(u=> u.Permission == 1 && u.Surname.Contains(surname) && u.Lastname.Contains(lastname))
            .Skip((nbPage-1)*nbElementsByPage)
            .Take(nbElementsByPage)
            .ToList();
    }

    public int FetchEmployeesFilteringCount(string surname, string lastname)
    {
        using var context = _contextProvider.NewContext();
        return context.Users.Count(u => u.Permission == 1 && u.Surname.Contains(surname) && u.Lastname.Contains(lastname));
    }

    public DbUser FetchById(int id)
    {
        using var context = _contextProvider.NewContext();
        var user = context.Users.FirstOrDefault(u => u.Id == id);

        if (user == null) throw new KeyNotFoundException($"User with Id {id} has not been found");

        return user;
    }

    public DbUser FetchByCredential(string email, string password)
    {
        using var context = _contextProvider.NewContext();
        
        var user = context.Users.FirstOrDefault(u => u.Email == email);
        
        if (user == null) throw new KeyNotFoundException($"User with this email and password has not been found");

        string hsh = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: Convert.FromBase64String(user!.Salt),
            prf:KeyDerivationPrf.HMACSHA256,
            iterationCount:100000,
            numBytesRequested:256/8
        ));
        
        if (hsh != user.Hsh) throw new KeyNotFoundException($"User with this email and password has not been found");

        return user;
    }

    public DbUser FetchUsernameByEmail(string email)
    {
        using var context = _contextProvider.NewContext();
        var user = context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null) throw new KeyNotFoundException($"User not found");
        return user;

    }

    public DbUser Create(string surname, string lastName, string email, DateTime birthdate, string password, int permission)
    {
        using var context = _contextProvider.NewContext();

        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        if (!Regex.IsMatch(email, emailRegex))
        {
            // invalid email
            throw new ArgumentException($"This email is not valid");
        }
        
        const string passwordRegex = @"^(?=.*\d)[a-zA-Z\d]{8,}$";

        if (!Regex.IsMatch(password, passwordRegex))
        {
            // invalid password
            throw new ArgumentException($"The password must at least have 8 characters and include 1 number");
        }

        // Check if the email is already used or not
        var userDb = context.Users.FirstOrDefault(u => u.Email == email);
        if (userDb != null)
            throw new ArgumentException($"This email is already used");
            
        // Transform the password to hashed password
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        var hsh = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf:KeyDerivationPrf.HMACSHA256,
            iterationCount:100000,
            numBytesRequested:256/8
            ));
        
        var user = new DbUser
        {
            Surname = surname,
            Lastname = lastName,
            Email = email,
            Birthdate = birthdate,
            Hsh = hsh,
            Permission = permission,
            Salt = Convert.ToBase64String(salt)
        };
        context.Users.Add(user);
        context.SaveChanges();
        return user;
    }

    public bool Delete(int id)
    {
        using var context = _contextProvider.NewContext();
        try
        {
            context.Users.Remove(new DbUser { Id = id });
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }
    }
    
    public bool Update(DbUser user)
    {
        using var context = _contextProvider.NewContext();

        try
        {
            context.Users.Update(user);
            return context.SaveChanges() == 1;
        }
        catch (DbUpdateConcurrencyException e)
        {
            return false;
        }        
    }
}