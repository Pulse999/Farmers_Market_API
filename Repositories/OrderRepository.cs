using Farmers_Market_API.Models;

namespace Farmers_Market_API.Repositories
{
    public class OrderRepository
    {
        private static List<Order> orders = new();

        public List<Order> GetAll()
        {
            return orders;
        }

        public Order? GetById(int id)
        {
            return orders.FirstOrDefault(o => o.OrderId == id);
        }

        public void Add(Order order)
        {
            order.OrderId = orders.Count + 1;
            orders.Add(order);
        }
    }
}