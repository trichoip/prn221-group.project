using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoeRepository _shoeRepository;

        public OrdersModel(IOrderRepository orderRepository, IShoeRepository shoeRepository)
        {
            _orderRepository = orderRepository;
            _shoeRepository = shoeRepository;
        }
        [BindProperty]
        public List<Order> Orders { get; set; }
        [BindProperty]
        public List<OrderItem> Items { get; set; }

        public async Task OnGetAsync()
        {
            var account = GetAccount();
            if (account == 0)
            {
                TempData["Message"] = "Please login to see your orders!!!";
                return;
            }
            TempData.Remove("Message");
            var order = await _orderRepository.GetOrderById(account);
            Orders = order;
        }
        public async Task<IActionResult> OnPost(int orderId)
        {
            var order = await _orderRepository.GetOrder(orderId);

            if (order == null)
            {
                return NotFound();
            }

            if (order.Status == "Pending")
            {
                order.Status = "Cancelled";
                foreach (var item in order.OrderItems)
                {
                    var shoe = _shoeRepository.GetShoe((int)item.ShoeId);
                    if (shoe == null)
                    {
                        return BadRequest();
                    }
                    shoe.Quantity = (int)(shoe.Quantity + item.Quantity);
                    _shoeRepository.UpdateShoe(shoe);
                }
                _orderRepository.UpdateOrder(order);
                TempData["Message"] = "Order successfully cancelled.";
            }
            else
            {
                TempData["Message"] = "Cannot cancel order. Invalid status.";
            }

            return RedirectToPage("./Orders");
        }
        private int GetAccount()
        {
            var id = HttpContext.Session.GetString("accountId");
            if (id == null)
            {
                return 0;
            }
            return int.Parse(id);
        }
    }
}
