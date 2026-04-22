using Farmers_Market_API.Models;
using Farmers_Market_API.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Farmers_Market_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmerController : ControllerBase
    {
        private static List<Farmer> farmers = new List<Farmer>
        {
            new Farmer { FarmerId = 1, FullName = "Kobus", Email = "kobus@example.com", PhoneNumber = "123", Location = "Pretoria", Province = Province.Gauteng, Rating = 4.5, IsVerified = true },
            new Farmer { FarmerId = 2, FullName = "Tyrique", Email = "tyrique@example.com", PhoneNumber = "456", Location = "Joburg", Province = Province.Gauteng, Rating = 4.0, IsVerified = true }
        };

        [HttpGet]
        public ActionResult<List<Farmer>> GetAll()
        {
            return Ok(farmers);
        }

        [HttpPost]
        public ActionResult Add(Farmer farmer)
        {
            farmer.FarmerId = farmers.Count + 1;
            farmers.Add(farmer);

            return Ok(farmer);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var farmer = farmers.FirstOrDefault(f => f.FarmerId == id);

            if (farmer == null)
                return NotFound("Farmer not found");

            farmers.Remove(farmer);

            return Ok("Deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Farmer updatedFarmer)
        {
            var farmer = farmers.FirstOrDefault(f => f.FarmerId == id);

            if (farmer == null)
                return NotFound("Farmer not found");

            farmer.FullName = updatedFarmer.FullName;
            farmer.Email = updatedFarmer.Email;
            farmer.PhoneNumber = updatedFarmer.PhoneNumber;
            farmer.Location = updatedFarmer.Location;
            farmer.Province = updatedFarmer.Province;
            farmer.IsVerified = updatedFarmer.IsVerified;

            return Ok(farmer);
        }
    }
}