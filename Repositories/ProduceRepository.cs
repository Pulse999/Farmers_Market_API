using Farmers_Market_API.Models;
using Farmers_Market_API.Enums;
using Farmers_Market_API.Exceptions;
using Farmers_Market_API.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Farmers_Market_API.Repositories
{
    public class ProduceRepository : IRepository<ProduceListing>
    {
        private readonly List<ProduceListing> _listings = new();

        public List<ProduceListing> GetAll()
        {
            return _listings;
        }

        public ProduceListing? GetById(int id)
        {
            return _listings.FirstOrDefault(x => x.ListingId == id);
        }

        public List<ProduceListing> GetAvailable()
        {
            return _listings.Where(x => x.IsAvailable).ToList();
        }

        public List<ProduceListing> GetByCategory(Category category)
        {
            return _listings.Where(x => x.Category == category).ToList();
        }

        public void Add(ProduceListing listing)
        {
            if (listing.QuantityKg <= 0)
                throw new System.ArgumentException("Quantity must be greater than 0.", nameof(listing.QuantityKg));

            listing.ListingId = _listings.Count + 1;
            _listings.Add(listing);
        }

        public bool Delete(int id)
        {
            var listing = _listings.FirstOrDefault(x => x.ListingId == id);

            if (listing == null)
                return false;

            _listings.Remove(listing);

            return true;
        }

        public bool SoftDelete(int id)
        {
            var listing = _listings.FirstOrDefault(x => x.ListingId == id);

            if (listing == null)
                return false;

            listing.IsAvailable = false;

            return true;
        }

        public List<ProduceListing> GetByPriceRange(double minPrice, double maxPrice)
        {
            return _listings
                .Where(x => x.PricePerKg >= minPrice &&
                            x.PricePerKg <= maxPrice)
                .ToList();
        }
    }
}