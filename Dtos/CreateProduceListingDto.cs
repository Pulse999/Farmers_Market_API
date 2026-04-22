using System.ComponentModel.DataAnnotations;
using Farmers_Market_API.Enums;

namespace Farmers_Market_API.DTOs
{
    public class CreateProduceListingDto
    {
        [Required]
        public int FarmerId { get; set; }

        [Required]
        [MinLength(3)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public Category Category { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double PricePerKg { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public double QuantityKg { get; set; }

        public DateTime HarvestDate { get; set; }

        public string? Description { get; set; }
    }
}