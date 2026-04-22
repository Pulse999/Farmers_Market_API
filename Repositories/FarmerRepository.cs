using System.Linq;
using Farmers_Market_API.Enums;
using Farmers_Market_API.Models;

namespace Farmers_Market_API.Repositories
{
    public class FarmerRepository
    {
        private static List<Farmer> farmers = new List<Farmer>
        {
            new Farmer { FarmerId = 1, FullName = "Kobus", Email = "kobus@example.com", PhoneNumber = "123", Location = "Pretoria", Province = Province.Gauteng, Rating = 4.5, IsVerified = true },
            new Farmer { FarmerId = 2, FullName = "Tyrique", Email = "tyrique@example.com", PhoneNumber = "456", Location = "Joburg", Province = Province.Gauteng, Rating = 4.0, IsVerified = true }
        };

        // Get all farmers
        public List<Farmer> GetAll()
        {
            return farmers;
        }

        // Create a new farmer
        public Farmer Create(Farmer farmer)
        {
            var nextId = farmers.Any() ? farmers.Max(f => f.FarmerId) + 1 : 1;
            farmer.FarmerId = nextId;
            farmers.Add(farmer);
            return farmer;
        }

        // Get farmer by id
        public Farmer? GetFarmerById(int id)
        {
            return farmers.FirstOrDefault(f => f.FarmerId == id);
        }

        // Get farmer by email
        public Farmer? GetFarmerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return farmers.FirstOrDefault(f => string.Equals(f.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        // Update existing farmer
        public Farmer? UpdateFarmer(int id, Farmer updatedFarmer)
        {
            var farmer = farmers.FirstOrDefault(f => f.FarmerId == id);

            if (farmer == null)
                return null;

            farmer.FullName = updatedFarmer.FullName;
            farmer.Email = updatedFarmer.Email;
            farmer.PhoneNumber = updatedFarmer.PhoneNumber;
            farmer.Location = updatedFarmer.Location;
            farmer.Province = updatedFarmer.Province;
            farmer.Rating = updatedFarmer.Rating;
            farmer.IsVerified = updatedFarmer.IsVerified;

            return farmer;
        }

        // Delete farmer by id
        public bool Delete(int id)
        {
            var farmer = farmers.FirstOrDefault(f => f.FarmerId == id);

            if (farmer == null)
                return false;

            farmers.Remove(farmer);
            return true;
        }
    }
}