using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.ReporitoryImp
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PRN_Shoes_StoreContext _context;



        public AccountRepository(PRN_Shoes_StoreContext context)
        {
            _context = context;
        }


        public async Task<List<Account>> FindAllAsync() => await _context.Accounts.ToListAsync();

        public Account? FindByEmail(string email)
        {
            return _context.Accounts.FirstOrDefault(account => account.Email == email);
        }

        public Account? FindByUsername(string username)
        {
            return _context.Accounts.FirstOrDefault(account => account.Username == username);
        }

        public Account? FindByUsernameOrEmailAndPassword(string username, string password)
        {
            return _context.Accounts.FirstOrDefault(account => (account.Username == username || account.Email == username) && account.Password == password);
        }

        public List<Account> listAllAccount()
        {
            return _context.Accounts.Where(m=> m.Role.Equals("user")).ToList();
        }

        public void Add(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        } 

        public Account GetAccountByID(int? ID)
        {
            return _context.Accounts.Where(m => m.AccountId == ID).FirstOrDefault();
        }


        public bool Delete(Account account)
        {

            Account accountUpdate = _context.Accounts.Where(x => x.AccountId.Equals(account.AccountId)).FirstOrDefault();
            if (accountUpdate != null)
            {
                try
                {
                    accountUpdate.IsActive = false;
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Update fail !!!");
                }
            }
            else
            {
                throw new Exception("Account not found !!!");
            }
        }


        public bool Update(Account account)
        {
            throw new NotImplementedException();
        }

    }
}
