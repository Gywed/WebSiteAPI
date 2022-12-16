using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IUserRepository
{
    IEnumerable<DbUser> FetchAll();

    IEnumerable<DbUser> FetchEmployeesFilteredPagination(int nbPage, int nbElementsByPage, string surname,
        string lastname);
    int FetchEmployeesFilteringCount(string surname, string lastName);

    DbUser FetchById(int id);

    DbUser FetchByCredential(string email, string password);
    DbUser FetchUsernameByEmail(string email);
    
    DbUser Create(string surname, string lastName, string email, DateTime birthdate, string password, int permission);
    
    bool Delete(int id);
    
    bool Update(DbUser user);
}