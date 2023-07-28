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
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;
        IAccountRepository _accountRepository;
        public DeleteModel(DataAccess.Models.PRN_Shoes_StoreContext context)
        {
            this._accountRepository = new AccountRepository(context);

            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Account = _accountRepository.GetAccountByID(id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (_accountRepository.Delete(Account))
            {
                return RedirectToPage("./Index");
            }

            return RedirectToPage("./Index");
        }
    }
}
