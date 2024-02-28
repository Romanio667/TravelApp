namespace TravelApp.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Place>? Places { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Image { get; set; }
    }
}
