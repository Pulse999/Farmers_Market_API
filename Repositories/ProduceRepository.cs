using Farmers_Market_API.Models;
using Farmers_Market_API.Enums;
using Farmers_Market_API.Exceptions;

namespace Farmers_Market_API.Repositories
{
    public class ProduceRepository
    {
        private readonly List<ProduceListing> _listings = new();

        public List<ProduceListing> GetAll()
        {
            return _listings;
        }

        public ProduceListing GetById(int id)
        {
            var listing = _listings.FirstOrDefault(x => x.ListingId == id);

            if (listing == null)
                throw new ListingNotFoundException(id);

            return listing;
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
            listing.ListingId = _listings.Count + 1;
            _listings.Add(listing);
        }
    }
}