using TravelApp.Models;

namespace TravelApp.Interfaces
{
    public interface IUserRatingRepository
    {
        void Add(UserRating userRating);
        void Update(UserRating userRating);
        UserRating GetByRatedUserAndRatedByUser(string ratedUserId, string ratedByUserId);
        Task<double> GetAverageRatingForUser(string userId);
    }
}
