using Farmers_Market_API.Models;
using Farmers_Market_API.Enums;
using Microsoft.AspNetCore.Mvc;
using Farmers_Market_API.Repositories;

namespace Farmers_Market_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmerController : ControllerBase
    {
        private FarmerRepository _repository = new FarmerRepository();

        // ✅ GET ALL
        [HttpGet]
        public ActionResult<List<Farmer>> GetAll()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error retrieving farmers", details = ex.Message });
            }
        }

        // ✅ GET BY ID (IMPORTANT ADD)
        [HttpGet("{id}")]
        public ActionResult<Farmer> GetById(int id)
        {
            var farmer = _repository.GetFarmerById(id);

            if (farmer == null)
                return NotFound("Farmer not found");

            return Ok(farmer);
        }

        // ✅ GET BY EMAIL (you already built this in repo 👌)
        [HttpGet("email/{email}")]
        public ActionResult<Farmer> GetByEmail(string email)
        {
            var farmer = _repository.GetFarmerByEmail(email);

            if (farmer == null)
                return NotFound("Farmer not found");

            return Ok(farmer);
        }

        // ✅ CREATE (IMPROVED)
        [HttpPost]
        public ActionResult<Farmer> Add(Farmer farmer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdFarmer = _repository.Create(farmer);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdFarmer.FarmerId },
                    createdFarmer
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error creating farmer", details = ex.Message });
            }
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public ActionResult Update(int id, Farmer updatedFarmer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var farmer = _repository.UpdateFarmer(id, updatedFarmer);

                if (farmer == null)
                    return NotFound("Farmer not found");

                return Ok(farmer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error updating farmer", details = ex.Message });
            }
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var success = _repository.Delete(id);

                if (!success)
                    return NotFound("Farmer not found");

                return Ok("Deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error deleting farmer", details = ex.Message });
            }
        }
    }
}