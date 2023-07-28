using DataAccess.Models;
using DataAccess.Repository;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        public RegisterModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [BindProperty]
        public RegisterVM RegisterVM { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (RegisterVM.Password != RegisterVM.ConfirmPassword)
            {
                ModelState.AddModelError("RegisterVM.ConfirmPassword", "Confirm does not match");
            }
            if (_accountRepository.FindByEmail(RegisterVM.Email) != null)
            {
                ModelState.AddModelError("RegisterVM.Email", "Email already exist");
            }
            if (_accountRepository.FindByUsername(RegisterVM.Username) != null)
            {
                ModelState.AddModelError("RegisterVM.Username", "Username already exist");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Account account = new Account
            {
                Username = RegisterVM.Username,
                Email = RegisterVM.Email,
                Password = RegisterVM.Password,
                FirstName = RegisterVM.FirstName,
                LastName = RegisterVM.LastName
            };
            _accountRepository.Add(account);

            return RedirectToPage("./Login");
        }
    }
}
