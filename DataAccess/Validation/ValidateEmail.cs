using DataAccess.ReporitoryImp;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Validation
{
    public class ValidateEmail:ValidationAttribute
    {
        IAccountRepository accountRepository;
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;

        public ValidateEmail() {
            ErrorMessage = "Exist Email!";
            accountRepository = new AccountRepository(_context);
            
            
        }
    }
}
