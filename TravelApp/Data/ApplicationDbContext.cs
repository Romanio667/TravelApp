using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelApp.Models;

namespace TravelApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<Place> Place { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<TripAddress> TripAddresses { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<Tip> Tips { get; set; }
    }
}
