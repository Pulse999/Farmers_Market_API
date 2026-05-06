using Farmers_Market_API.Enums;

namespace Farmers_Market_API.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int ListingId { get; set; }

        public int BuyerId { get; set; }

        public double QuantityKg { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}