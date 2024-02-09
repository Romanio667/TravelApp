using TravelApp.Models;

namespace TravelApp.Interfaces
{
    public interface ITripRepository
    {
        Task<IEnumerable<Trip>> GetAll();
        Task<Trip> GetByIdAsync(int id);
        Task<IEnumerable<Trip>> GetAllTripsByCity(string city);
        bool Add(Trip trip);
        bool Update(Trip trip);
        bool Delete(Trip trip);
        bool Save();
    }
}
