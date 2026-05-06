using Farmers_Market_API.Enums;

namespace Farmers_Market_API.Models
{
    public class Buyer : Person
    {
        public int BuyerId { get; set; }

        public BuyerType BuyerType { get; set; }

        public string Location { get; set; } = string.Empty;

        public override string GetContactInfo()
        {
            return $"{FullName} ({BuyerType}) | Email: {Email} | Phone: {PhoneNumber}";
        }
    }
}