namespace TravelApp.Models
{
    public class UserRating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RatedByUserId { get; set; }
        public int Rating { get; set; }
    }
}
