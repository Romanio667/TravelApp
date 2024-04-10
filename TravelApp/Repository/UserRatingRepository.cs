// UserRatingRepository.cs
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TravelApp.Data;
using TravelApp.Interfaces;
using TravelApp.Models;

namespace TravelApp.Repository
{
    public class UserRatingRepository : IUserRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(UserRating userRating)
        {
            _context.UserRatings.Add(userRating);
            _context.SaveChanges();
        }

        public void Update(UserRating userRating)
        {
            _context.UserRatings.Update(userRating);
            _context.SaveChanges();
        }

        public UserRating GetByRatedUserAndRatedByUser(string ratedUserId, string ratedByUserId)
        {
            return _context.UserRatings.FirstOrDefault(ur => ur.UserId == ratedUserId && ur.RatedByUserId == ratedByUserId);
        }

        public async Task<double> GetAverageRatingForUser(string userId)
        {
            var ratings = await _context.UserRatings
                                .Where(ur => ur.UserId == userId)
                                .ToListAsync();

            if (ratings.Any())
            {
                return await _context.UserRatings
                                    .Where(r => r.UserId == userId)
                                    .AverageAsync(r => r.Rating);
            }
            else
            {
                return 0; 
            }

        }
    }
}
