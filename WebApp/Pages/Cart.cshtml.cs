using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static WebApp.Constrains;

namespace WebApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly IShoeRepository _shoeRepository;
        private readonly IOrderRepository _orderRepository;
        public CartModel(IShoeRepository shoeRepository, IOrderRepository orderRepository)
        {
            _shoeRepository = shoeRepository;
            _orderRepository = orderRepository;
        }
        [BindProperty]
        public int TotalItem { get; set; }
        [BindProperty]
        public decimal? Total { get; set; }
        [BindProperty]
        public List<OrderItem> Cart { get; set; }
        public void OnGet()
        {
            var accountId = GetAccountId();
            var cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, $"cart_{accountId}");
            if (cart != null)
            {
                Total = cart.Sum(i => i.Shoe.Price * i.Quantity);
                TotalItem = cart.Count;
                Cart = cart;
            }
            else
            {
                TempData["Message"] = "Cart is empty!";
            }
        }

        public IActionResult OnGetBuyNow(int id)
        {
            var accountId = GetAccountId();
            var cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, $"cart_{accountId}");
            var shoe = _shoeRepository.GetShoe(id);
            if (cart == null)
            {
                cart = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ShoeId = id,
                        Shoe = shoe,
                        Price = shoe.Price,
                        Quantity = 1
                    }
                };
                var value = JsonConvert.SerializeObject(cart);
                HttpContext.Session.SetString($"cart_{accountId}", value);
            }
            else
            {
                int index = Exists(cart, id);
                if (index == -1)
                {
                    cart.Add(new OrderItem
                    {
                        ShoeId = id,
                        Shoe = shoe,
                        Price = shoe.Price,
                        Quantity = 1
                    });
                }
                else
                {
                    cart[index].Quantity++;
                }
                var value = JsonConvert.SerializeObject(cart);
                HttpContext.Session.SetString($"cart_{accountId}", value);
            }
            return RedirectToPage("Cart");
        }
        public IActionResult OnGetDelete(int id)
        {
            var accountId = GetAccountId();
            var cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, $"cart_{accountId}");
            int index = Exists(cart, id);
            cart.RemoveAt(index);
            if(cart.Count == 0)
            {
                TempData["Message"] = "Cart is empty!";
                HttpContext.Session.Remove($"cart_{accountId}");
                return RedirectToPage("Cart");
            }
            var value = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString($"cart_{accountId}", value);
            return RedirectToPage("Cart");
        }

        public IActionResult OnPostUpdate(int[] quantities)
        {
            var accountId = GetAccountId();
            var cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, $"cart_{accountId}");
            for (var i = 0; i < cart.Count; i++)
            {
                cart[i].Quantity = quantities[i];
            }
            var value = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString($"cart_{accountId}", value);
            return RedirectToPage("Cart");
        }
        public async Task<IActionResult> OnPostCheckOut()
        {
            var accountId = GetAccountId();
            if(accountId == 0)
            {
                TempData["Message"] = "You have to login!";
                return RedirectToPage("Cart");
            }
            var cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, $"cart_{accountId}");
            var orderItems = new List<OrderItem>();
            foreach (var item in cart)
            {
                var shoe = _shoeRepository.GetShoe(item.Shoe.ShoeId);
                if(shoe == null)
                {
                    TempData["Error"] = "Error when get shoe";
                    return RedirectToPage("Cart");
                }
                if(item.Quantity < 0 || item.Quantity > shoe.Quantity)
                {
                    TempData["Error"] = $"Only have {shoe.Quantity} in our stock!!!";
                    return RedirectToPage("Cart");
                }
                orderItems.Add(new OrderItem
                {
                    ShoeId = item.Shoe.ShoeId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                });
                shoe.Quantity = (int)(shoe.Quantity - item.Quantity);
                _shoeRepository.UpdateShoe(shoe);
            }
            var order = new Order
            {
                AccountId = accountId,
                OrderDate = DateTime.UtcNow,
                Total = Total = cart.Sum(i => i.Shoe.Price * i.Quantity),
                Status = OrderStatus.PENDING,
                OrderItems = orderItems
            };
            var result = await _orderRepository.AddOrder(order);
            if (result)
            {
                HttpContext.Session.Remove($"cart_{accountId}");
                TempData["OrderMessage"] = "Your order is proccessing!";
                return RedirectToPage("Cart");
            }
            return RedirectToPage("Cart");
        }

        private int GetAccountId()
        {
            var id = HttpContext.Session.GetString("accountId");
            if (id == null)
                return 0;
            return int.Parse(id);
        }
        private int Exists(List<OrderItem> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].Shoe.ShoeId == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
