namespace TravelApp.Models
{
    public class Tip
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int PlaceId { get; set; }
        public string UserId { get; set; }
        public Place Place { get; set; } // Ссылка на место
    }
}
