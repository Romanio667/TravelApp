namespace TravelApp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? Age { get; set; }
        public int? Mileage { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string ProfileImageUrl { get; set; }    
    }
}
