using System.ComponentModel.DataAnnotations.Schema;
using TravelApp.Data.Enum;

namespace TravelApp.Models
{
    public class TripEditModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? TripFee { get; set; }
        public string? Currency { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int TripAddressId { get; set; }
        public TripAddress TripAddress { get; set; }
        public TripCategory TripCategory { get; set; }
    }
}

