using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TravelApp.Data.Enum;
using TravelApp.Models;

namespace TravelApp.Models
{
        public class Place
        {
            [Key]
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? Image { get; set; }
            [ForeignKey("Address")]
            public int AddressId { get; set; }
            public Address? Address { get; set; }
            public PlaceCategory PlaceCategory { get; set; }
            [ForeignKey("AppUser")]
            public string? AppUserId { get; set; }
            public AppUser? AppUser { get; set; }
        }
    }

