using System.ComponentModel.DataAnnotations.Schema;
using TravelApp.Data.Enum;

namespace TravelApp.Models
{
    public class TripEditModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public TripCategory TripCategory { get; set; }
    }
}

