using TravelApp.Models;

namespace TravelApp.Interfaces
{
    public interface ITipRepository
    {
        Task<IEnumerable<Tip>> GetTipsForPlace(int placeId);
        Task Add(Tip tip);
        Task Update(Tip tip);
        Task Delete(int id);
        Task<Tip> GetByIdAsync(int id);
    }
}
