using Microsoft.AspNetCore.Mvc;
using Farmers_Market_API.Models;
using Farmers_Market_API.Repositories;
using Farmers_Market_API.Enums;

namespace Farmers_Market_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private static OrderRepository _orders = new();
        private static ProduceRepository _produce = new();

        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            var listing = _produce.GetById(order.ListingId);

            if (listing == null)
                return NotFound("Listing not found");

            if (order.QuantityKg > listing.QuantityKg)
                return UnprocessableEntity("Order quantity exceeds available stock");

            order.Status = OrderStatus.Pending;

            _orders.Add(order);

            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = _orders.GetById(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPatch("{id}/confirm")]
        public ActionResult Confirm(int id)
        {
            var order = _orders.GetById(id);

            if (order == null)
                return NotFound();

            order.Status = OrderStatus.Confirmed;

            return Ok(order);
        }

        [HttpPatch("{id}/collect")]
        public ActionResult Collect(int id)
        {
            var order = _orders.GetById(id);

            if (order == null)
                return NotFound();

            if (order.Status != OrderStatus.Confirmed)
                return BadRequest("Only confirmed orders can be collected.");

            var listing = _produce.GetById(order.ListingId);

            if (listing == null)
                return NotFound("Listing not found");

            listing.QuantityKg -= order.QuantityKg;

            if (listing.QuantityKg <= 0)
                listing.IsAvailable = false;

            order.Status = OrderStatus.Collected;

            return Ok(order);
        }
    }
}