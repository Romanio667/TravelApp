using TravelApp.Models;

namespace TravelApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Trip>> GetAllUserTrips();
        Task<List<Place>> GetAllUserPlaces();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
