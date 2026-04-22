using Microsoft.AspNetCore.Mvc;
using Farmers_Market_API.Models;
using Farmers_Market_API.Repositories;
using Farmers_Market_API.Enums;
using Farmers_Market_API.Exceptions;
using Farmers_Market_API.DTOs;

namespace Farmers_Market_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduceController : ControllerBase
    {
        private static ProduceRepository _repo = new ProduceRepository();

        // GET ALL
        [HttpGet]
        public ActionResult<List<ProduceListingResponseDto>> GetAll()
        {
            var listings = _repo.GetAll()
                .Select(l => new ProduceListingResponseDto
                {
                    ListingId = l.ListingId,
                    FarmerId = l.FarmerId,
                    ProductName = l.ProductName,
                    Category = l.Category,
                    PricePerKg = l.PricePerKg,
                    QuantityKg = l.QuantityKg,
                    IsAvailable = l.IsAvailable,
                    HarvestDate = l.HarvestDate,
                    DateListed = l.DateListed,
                    Description = l.Description
                }).ToList();

            return Ok(listings);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public ActionResult<ProduceListingResponseDto> GetById(int id)
        {
            try
            {
                var l = _repo.GetById(id);

                var dto = new ProduceListingResponseDto
                {
                    ListingId = l.ListingId,
                    FarmerId = l.FarmerId,
                    ProductName = l.ProductName,
                    Category = l.Category,
                    PricePerKg = l.PricePerKg,
                    QuantityKg = l.QuantityKg,
                    IsAvailable = l.IsAvailable,
                    HarvestDate = l.HarvestDate,
                    DateListed = l.DateListed,
                    Description = l.Description
                };

                return Ok(dto);
            }
            catch (ListingNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET AVAILABLE
        [HttpGet("available")]
        public ActionResult<List<ProduceListingResponseDto>> GetAvailable()
        {
            var listings = _repo.GetAvailable()
                .Select(l => new ProduceListingResponseDto
                {
                    ListingId = l.ListingId,
                    FarmerId = l.FarmerId,
                    ProductName = l.ProductName,
                    Category = l.Category,
                    PricePerKg = l.PricePerKg,
                    QuantityKg = l.QuantityKg,
                    IsAvailable = l.IsAvailable,
                    HarvestDate = l.HarvestDate,
                    DateListed = l.DateListed,
                    Description = l.Description
                }).ToList();

            return Ok(listings);
        }

        // GET BY CATEGORY
        [HttpGet("category/{category}")]
        public ActionResult<List<ProduceListingResponseDto>> GetByCategory(Category category)
        {
            var results = _repo.GetByCategory(category);

            if (results.Count == 0)
                return NotFound("No listings found for this category.");

            var listings = results.Select(l => new ProduceListingResponseDto
            {
                ListingId = l.ListingId,
                FarmerId = l.FarmerId,
                ProductName = l.ProductName,
                Category = l.Category,
                PricePerKg = l.PricePerKg,
                QuantityKg = l.QuantityKg,
                IsAvailable = l.IsAvailable,
                HarvestDate = l.HarvestDate,
                DateListed = l.DateListed,
                Description = l.Description
            }).ToList();

            return Ok(listings);
        }

        // POST (WITH DTO + VALIDATION)
        [HttpPost]
        public ActionResult<ProduceListingResponseDto> Add([FromBody] CreateProduceListingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var listing = new ProduceListing
            {
                FarmerId = dto.FarmerId,
                ProductName = dto.ProductName,
                Category = dto.Category,
                PricePerKg = dto.PricePerKg,
                QuantityKg = dto.QuantityKg,
                HarvestDate = dto.HarvestDate,
                Description = dto.Description,
                DateListed = DateTime.Now,
                IsAvailable = true
            };

            _repo.Add(listing);

            var response = new ProduceListingResponseDto
            {
                ListingId = listing.ListingId,
                FarmerId = listing.FarmerId,
                ProductName = listing.ProductName,
                Category = listing.Category,
                PricePerKg = listing.PricePerKg,
                QuantityKg = listing.QuantityKg,
                IsAvailable = listing.IsAvailable,
                HarvestDate = listing.HarvestDate,
                DateListed = listing.DateListed,
                Description = listing.Description
            };

            return CreatedAtAction(nameof(GetById), new { id = listing.ListingId }, response);
        }

        // BONUS: REVENUE
        [HttpGet("{id}/revenue")]
        public ActionResult<double> GetRevenue(int id)
        {
            try
            {
                var listing = _repo.GetById(id);
                return Ok(listing.CalculateRevenue());
            }
            catch (ListingNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}