using DataAccess.Models;

namespace DataAccess.Repository
{
    public interface IOrderItemRepository
    {
        Task<bool> AddOrderItem(OrderItem orderItem);
        void UpdateOrderItem(OrderItem orderItem);
        void DeleteOrderItem(int orderItemId);
        Task<List<OrderItem>> GetOrderItemById(int orderId);
    }
}
