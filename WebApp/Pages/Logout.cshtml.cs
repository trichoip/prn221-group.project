using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            string role = HttpContext.Session.GetString(Constrains.SessionAttribute.AUTH_USER_ROLE);
            if (role == null)
            {
                return RedirectToPage("./Index");
            }
            HttpContext.Session.Remove(Constrains.SessionAttribute.AUTH_USER_ROLE);
            HttpContext.Session.Remove(Constrains.SessionAttribute.AUTH_USER_NAME);
            HttpContext.Session.Remove(Constrains.SessionAttribute.AUTH_USER_FIRST_NAME);
            HttpContext.Session.Remove("accountIdInt");
            HttpContext.Session.Remove("accountId");

            if (role == Constrains.AccountRole.ADMIN)
            {
                return RedirectToPage("./Admin/Login");
            }
            else
            {
                return RedirectToPage("./Login");
            }
        }
    }
}
