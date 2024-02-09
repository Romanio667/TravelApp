using Microsoft.AspNetCore.Identity;
using TravelApp.Data.Enum;
using TravelApp.Models;

namespace TravelApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Place.Any())
                {
                    context.Place.AddRange(new List<Place>()
                    {
                        new Place()
                        {
                            Title = "Place 1",
                            Image = "first.png",
                            Description = "This is the description",
                            PlaceCategory = PlaceCategory.monument,
                            Address = new Address()
                            {
                                Country = "Russia",
                                City = "Novosibirsk",
                                Street = "Svechnikova 2"
                            }
                         },
                        new Place()
                        {
                            Title = "Place 2",
                            Image = "first.png",
                            Description = "This is the description",
                            PlaceCategory = PlaceCategory.amusementPark,
                            Address = new Address()
                            {
                                Country = "Russia",
                                City = "Novosibirsk",
                                Street = "Svechnikova 2"
                            }
                        },
                        new Place()
                        {
                            Title = "Place 3",
                            Image = "first.png",
                            Description = "This is the description",
                            PlaceCategory = PlaceCategory.cityPark,
                            Address = new Address()
                            {
                                Country = "Russia",
                                City = "Novosibirsk",
                                Street = "Svechnikova 2"
                            }
                        },
                        new Place()
                        {
                            Title = "Place 4",
                            Image = "first.png",
                            Description = "This is the description",
                            PlaceCategory = PlaceCategory.monument,
                            Address = new Address()
                            {
                                Country = "Russia",
                                City = "Novosibirsk",
                                Street = "Svechnikova 2"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Trip.Any())
                {
                    context.Trip.AddRange(new List<Trip>()
                    {
                        new Trip()
                        {
                            Title = "Trip 1",
                            Image = "first.png",
                            Description = "This is the description",
                            TripCategory = TripCategory.Short,
                            Address = new Address()
                            {
                                Country = "Russia",
                                City = "Novosibirsk",
                                Street = "Svechnikova 2"
                            }
                        },
                        new Trip()
                        {
                            Title = "Trip 2",
                            Image = "first.png",
                            Description = "This is the description",
                            TripCategory = TripCategory.Medium,
                            AddressId = 5,
                            Address = new Address()
                            {
                                Country = "Russia",
                                City = "Novosibirsk",
                                Street = "Svechnikova 2"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Country = "Russia",
                            City = "Novosibirsk",
                            Street = "Svechnikova 3"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Country = "Russia",
                            City = "Novosibirsk",
                            Street = "Svechnikova 3"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
