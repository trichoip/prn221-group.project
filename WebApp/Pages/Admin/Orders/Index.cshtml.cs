using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public IndexModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IList<Order> Order { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchStatus { get; set; }

        public async Task OnGetAsync()
        {
            Order = await _orderRepository.GetAllOrder();
            // Set up the search options for status
            ViewData["StatusOptions"] = new SelectList(new List<string>
            {
                "Pending",
                "Delivering",
                "Completed",
                "Cancelled",
                "Deleted"
            });

            // Handle search by status
            if (!string.IsNullOrEmpty(SearchStatus))
            {
                // Filter the orders by the selected status
                Order = Order.Where(o => o.Status == SearchStatus).ToList();
            }
        }
    }
}
