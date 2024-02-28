using Microsoft.EntityFrameworkCore;
using TravelApp.Data;
using TravelApp.Interfaces;
using TravelApp.Models;

namespace TravelApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Place>> GetAllUserPlaces()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userPlaces = _context.Place.Where(r => r.AppUser.Id == curUser);
            return userPlaces.ToList();
        }

        public async Task<List<Trip>> GetAllUserTrips()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userTrips = _context.Trip.Where(r => r.AppUser.Id == curUser);
            return userTrips.ToList();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
