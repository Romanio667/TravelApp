using Microsoft.EntityFrameworkCore;
using TravelApp.Data;
using TravelApp.Interfaces;
using TravelApp.Models;

namespace TravelApp.Repository
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly ApplicationDbContext _context;

        public PlaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Place place)
        {
            _context.Add(place);
            return Save();
        }

        public bool Delete(Place place)
        {
            _context.Remove(place);
            return Save();
        }

        public async Task<IEnumerable<Place>> GetAll()
        {
            return await _context.Place.ToListAsync();
        }

        public async Task<Place> GetByIdAsync(int id)
        {
            return await _context.Place.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Place>> GetPlaceByCity(string city)
        {
            return await _context.Place.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Place place)
        {
            _context.Update(place);
            return Save();
        }
    }
}
