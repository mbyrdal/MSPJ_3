using RazorPagesToMVCMigration.Models;

namespace RazorPagesToMVCMigration.DAL
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        void Save();
    }
}
