namespace TravelApp.Models
{
    public class EditUserDashboardViewModel
    {
        public string Id { get; set; }
        public int? Age { get; set; }
        public int? Mileage { get; set; }
        public string? ProfileImageURL { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public IFormFile Image { get; set; }
    }
}
