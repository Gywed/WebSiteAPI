using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IUserRepository
{
    IEnumerable<DbUser> FetchAll();
    
    DbUser FetchById(int id);

    DbUser FetchByCredential(string email, string password);
    
    DbUser Create(string surname, string lastName, string email, int age, string password, int permission);

    IEnumerable<DbUser> FetchAllEmployees();
}