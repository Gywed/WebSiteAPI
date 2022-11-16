using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IUserRepository
{
    IEnumerable<DbUser> FetchAll();

    IEnumerable<DbUser> FetchPaginationEmployee(int coef, int nbElements);

    DbUser FetchById(int id);

    DbUser FetchByCredential(string email, string password);
    
    DbUser Create(string surname, string lastName, string email, int age, string password, int permission);

    IEnumerable<DbUser> FetchAllEmployees();

    bool Delete(int id);
}