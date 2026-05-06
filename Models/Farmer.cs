namespace Farmers_Market_API.Models
{
    public class Farmer : Person
    {
        public int FarmerId { get; set; }

        public string FarmName { get; set; } = string.Empty;

        public FarmerLocation Location { get; set; }

        public double Rating { get; set; }

        public bool IsVerified { get; set; }

        public override string GetContactInfo()
        {
            return $"{FullName} from {FarmName} | Email: {Email} | Phone: {PhoneNumber}";
        }
    }
}