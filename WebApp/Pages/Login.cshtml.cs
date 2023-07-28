using DataAccess.Repository;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;
        public LoginModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public LoginVM LoginVM { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString(Constrains.SessionAttribute.AUTH_USER_NAME) == null)
            {
                return Page();
            }
            else
            {
                return RedirectToPage("./Index");
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var account = _accountRepository.FindByUsernameOrEmailAndPassword(LoginVM.UserName, LoginVM.Password);
            if (account == null)
            {
                ModelState.AddModelError("LoginVM.UserName", "Username or password not correct");
            }
            else if (account.IsActive == false)
            {
                ModelState.AddModelError("LoginVM.UserName", "Your account has been banned");
            }
            else if (account.Role != Constrains.AccountRole.USER)
            {
                ModelState.AddModelError("LoginVM.UserName", "Your account does not have permission to login");
            }
            else
            {
                HttpContext.Session.SetString(Constrains.SessionAttribute.AUTH_USER_ROLE, account.Role);
                HttpContext.Session.SetString("accountId", account.AccountId.ToString());
                HttpContext.Session.SetInt32("accountIdInt", account.AccountId);
                HttpContext.Session.SetString(Constrains.SessionAttribute.AUTH_USER_NAME, account.Username);
                HttpContext.Session.SetString(Constrains.SessionAttribute.AUTH_USER_FIRST_NAME, account.FirstName);
            }
            if (ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }

        }
    }
}
