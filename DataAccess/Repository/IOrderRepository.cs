using DataAccess.Models;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(Order order);
        void DeleteOrder(Order order);
        Task<List<Order>> GetAllOrder();
        void UpdateOrder(Order order);
        Task<List<Order>> GetOrderById(int id);
        Task<Order> GetOrder(int id);
    }
}
