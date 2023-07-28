using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Models;
using DataAccess.ReporitoryImp;
using DataAccess.Repository;
using DataAccess.ViewModels;

namespace WebApp.Pages.Admin.AccountManagement
{
    public class CreateModel : PageModel
    {
        IAccountRepository _accountRepository;
        private readonly DataAccess.Models.PRN_Shoes_StoreContext _context;
        public string ErrorMessage { get; set; }
        public CreateModel(DataAccess.Models.PRN_Shoes_StoreContext context)
        {
            this._accountRepository = new AccountRepository(context);
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateAccountVM CreateAccountVM { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_accountRepository.FindByEmail(CreateAccountVM.Email) != null)
            {

                ModelState.AddModelError("CreateAccountVM.Email", "Email already exist");
            }
            if (_accountRepository.FindByUsername(CreateAccountVM.Username) != null)
            {
                ModelState.AddModelError("CreateAccountVM.Username", "Username already exist");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Account account = new Account
            {
                Username = CreateAccountVM.Username,
                Email = CreateAccountVM.Email,
                Password = CreateAccountVM.Password,
                FirstName = CreateAccountVM.FirstName,
                LastName = CreateAccountVM.LastName
            };
            _accountRepository.Add(account);

            return RedirectToPage("./Index");
        }
    }
}
