using TravelApp.Data.Enum;

namespace TravelApp.Models
{
    public class PlaceEditModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public int AddressId {  get; set; }
        public Address Address { get; set; }
        public PlaceCategory PlaceCategory { get; set; }
    }
}
