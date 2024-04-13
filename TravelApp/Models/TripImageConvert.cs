using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TravelApp.Data.Enum;

namespace TravelApp.Models
{
    public class TripImageConvert
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? photo { get; set; }
        public DateTime? StartTime { get; set; }
        public int? TripFee { get; set; }
        public CurrencyCategory CurrencyCategory { get; set; }
        public string? Website { get; set; }
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Contact { get; set; }
        [ForeignKey("TripAddress")]
        public int TripAddressId { get; set; }
        public TripAddress TripAddress { get; set; }
        public TripCategory TripCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
