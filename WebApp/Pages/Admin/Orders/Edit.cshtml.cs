using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Models;
using DataAccess.Repository;

namespace WebApp.Pages.Admin.Orders
{
    public class EditModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAccountRepository _accountRepository;

        public EditModel(IOrderRepository orderRepository, IAccountRepository accountRepository)
        {
            _orderRepository = orderRepository;
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var order = await _orderRepository.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            var account = await _accountRepository.FindAllAsync();
            Order = order;
            ViewData["AccountId"] = new SelectList(account.Where(x => x.AccountId == order.AccountId), "AccountId", "Email");
            ViewData["Status"] = new SelectList(new List<string>
            {
            "Pending",
            "Delivering",
            "Completed",
            "Cancelled",
            "Deleted"
            });
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _orderRepository.UpdateOrder(Order);

            return RedirectToPage("./Index");
        }
    }
}
