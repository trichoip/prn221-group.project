using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.ReporitoryImp
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PRN_Shoes_StoreContext _context;
        public OrderRepository(PRN_Shoes_StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public Task<List<Order>> GetAllOrder()
        {
            return _context.Orders.OrderByDescending(x => x.OrderDate).Include(x => x.Account).Include(x => x.OrderItems).ThenInclude(x => x.Shoe).ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.OrderId == id);
        }

        public async Task<List<Order>> GetOrderById(int id)
        {
            return await _context.Orders.OrderByDescending(x => x.OrderDate).Where(x => x.AccountId == id).Include(x => x.OrderItems).ThenInclude(x => x.Shoe).ToListAsync();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}
