using DataAccess.Models;

namespace DataAccess.Repository
{
    public interface IAccountRepository
    {
        Account? FindByUsernameOrEmailAndPassword(string username, string password);
        Account? FindByEmail(string email);
        Account? FindByUsername(string username);
        Task<List<Account>> FindAllAsync();
        public List<Account> listAllAccount();
        public void Add(Account account);
        public bool Delete(Account account);
        public bool Update(Account account);
        public Account GetAccountByID(int? staffID);

    }
}
