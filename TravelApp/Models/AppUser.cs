using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace TravelApp.Models
{
    public class AppUser : IdentityUser
    {
      public string? Name { get; set; }
      public int? Age { get; set; }
      public string? ProfileImageURL {  get; set; }    
      public string? Country { get; set; }
      public string? City { get; set; }
      [ForeignKey("Address")]
      public int? AddressId { get; set; }
      public Address? Address { get; set; }
      public ICollection<Place> Place { get; set; }
      public ICollection<Trip> Trip { get; set; }
    }
}
