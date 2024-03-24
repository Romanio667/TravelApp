using System.ComponentModel.DataAnnotations;

namespace TravelApp.Models
{
    public class TripAddress
    {
        [Key]
        public int Id { get; set; }
        public string DepartureCountry { get; set; }
        public string DepartureCity { get; set; }
        public string DepartureStreet { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string ArrivalCountry { get; set; }
        public string ArrivalCity { get; set; }
        public string ArrivalStreet { get; set; }
        public DateTime? ArrivalTime { get; set; }
    }
}
