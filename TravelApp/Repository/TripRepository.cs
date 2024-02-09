using Microsoft.EntityFrameworkCore;
using TravelApp.Data;
using TravelApp.Interfaces;
using TravelApp.Models;

namespace TravelApp.Repository
{
    public class TripRepository : ITripRepository
    {
        private readonly ApplicationDbContext _context;

        public TripRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Trip trip)
        {
            _context.Add(trip);
            return Save();
        }

        public bool Delete(Trip trip)
        {
            _context.Remove(trip);
            return Save();
        }

        public async Task<IEnumerable<Trip>> GetAll()
        {
            return await _context.Trip.ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetAllTripsByCity(string city)
        {
            return await _context.Trip.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Trip> GetByIdAsync(int id)
        {
            return await _context.Trip.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Trip trip)
        {
            _context.Update(trip);
            return Save();
        }
    }
}
