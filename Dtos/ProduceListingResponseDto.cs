using Farmers_Market_API.Enums;

namespace Farmers_Market_API.DTOs
{
    public class ProduceListingResponseDto
    {
        public int ListingId { get; set; }
        public int FarmerId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public Category Category { get; set; }
        public double PricePerKg { get; set; }
        public double QuantityKg { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime HarvestDate { get; set; }
        public DateTime DateListed { get; set; }
        public string? Description { get; set; }
    }
}