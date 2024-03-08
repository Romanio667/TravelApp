using TravelApp.Models;

namespace TravelApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Trip>> GetAllUserTrips();
        Task<List<Place>> GetAllUserPlaces();
        Task<List<Place>> GetUserPlaces(string userId);
        Task<List<Trip>> GetUserTrips(string userId);
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
