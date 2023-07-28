using DataAccess.Repository;
using DataAccess.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebApp.Pages.Admin
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
                return Page();           
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
            else if (account.Role != Constrains.AccountRole.ADMIN)
            {
                ModelState.AddModelError("LoginVM.UserName", "Your account does not have permission to login");
            }
            else
            {
                HttpContext.Session.SetString(Constrains.SessionAttribute.AUTH_USER_ROLE, account.Role);
                HttpContext.Session.SetString("accountId", account.AccountId.ToString());
                HttpContext.Session.SetString(Constrains.SessionAttribute.AUTH_USER_NAME, account.Username);
                HttpContext.Session.SetString(Constrains.SessionAttribute.AUTH_USER_FIRST_NAME, account.FirstName);
                HttpContext.Session.SetString("ACCOUNT", JsonConvert.SerializeObject(account));
            }
            if (ModelState.IsValid)
            {
                return RedirectToPage("/Admin/AccountManagement/Index");
            }
            else
            {
                return Page();
            }

        }
    }
}
