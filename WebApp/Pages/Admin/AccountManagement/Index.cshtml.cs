using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using DataAccess.ReporitoryImp;
using DataAccess.Repository;

namespace WebApp.Pages.Admin.AccountManagement
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;
        IAccountRepository _accountRepository;
        public IndexModel(DataAccess.Models.PRN_Shoes_StoreContext context)
        {
            this._accountRepository = new AccountRepository(context);
            _context = context;
        }

        public IList<Account> Account { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Accounts != null)
            {
                Account = _accountRepository.listAllAccount();
            }
        }
    }
}
