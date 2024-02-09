using TravelApp.Models;

namespace TravelApp.Interfaces
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Place>> GetAll();
        Task<Place> GetByIdAsync(int id);
        Task<IEnumerable<Place>> GetPlaceByCity(string city);
        bool Add(Place place);
        bool Update(Place place);
        bool Delete(Place place);
        bool Save();
    }
}
