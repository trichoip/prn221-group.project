using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.ReporitoryImp
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly PRN_Shoes_StoreContext _context;
        public OrderItemRepository(PRN_Shoes_StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrderItem(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
               return true;
            }
            return false;
        }

        public async void DeleteOrderItem(int orderItemId)
        {
            var item = await _context.OrderItems.Where(x => x.OrderId == orderItemId).FirstOrDefaultAsync();
            if(item != null)
            {
                _context.OrderItems.Remove(item);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public Task<List<OrderItem>> GetOrderItemById(int orderId)
        {
            return _context.OrderItems.Where(x=>x.OrderId == orderId).ToListAsync();
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            _context.SaveChanges();
        }
    }
}
