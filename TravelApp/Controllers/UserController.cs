using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Interfaces;
using TravelApp.Models;
using TravelApp.Repository;

namespace TravelApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDashboardRepository _dashboardRepository;

        public UserController(IUserRepository userRepository, UserManager<AppUser> userManager, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor,
            IDashboardRepository dashboardRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;   
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Age = user.Age,
                    Country = user.Country,
                   ProfileImageUrl = user.ProfileImageURL,
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var userTrips = await _dashboardRepository.GetUserTrips(id);
            var userPlaces = await _dashboardRepository.GetUserPlaces(id);

            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Age = user.Age,
                ProfileImageUrl = user.ProfileImageURL ?? "/img/avatar-male-4.jpg",
                Country = user.Country,
                City = user.City,
                Places = userPlaces,
                Trips = userTrips
            };
            return View(userDetailViewModel);
        }

        /* [HttpGet]
         [Authorize]
         public async Task<IActionResult> EditProfile()
         {
             var user = await _userManager.GetUserAsync(User);

             if (user == null)
             {
                 return View("Error");
             }

             var editMV = new EditProfileViewModel()
             {
                 City = user.City,
                 State = user.State,
                 Pace = user.Pace,
                 Mileage = user.Mileage,
                 ProfileImageUrl = user.ProfileImageUrl,
             };
             return View(editMV);
         }

         [HttpPost]
         [Authorize]
         public async Task<IActionResult> EditProfile(EditProfileViewModel editVM)
         {
             if (!ModelState.IsValid)
             {
                 ModelState.AddModelError("", "Failed to edit profile");
                 return View("EditProfile", editVM);
             }

             var user = await _userManager.GetUserAsync(User);

             if (user == null)
             {
                 return View("Error");
             }

             if (editVM.Image != null) // only update profile image
             {
                 var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                 if (photoResult.Error != null)
                 {
                     ModelState.AddModelError("Image", "Failed to upload image");
                     return View("EditProfile", editVM);
                 }

                 if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                 {
                     _ = _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                 }

                 user.ProfileImageUrl = photoResult.Url.ToString();
                 editVM.ProfileImageUrl = user.ProfileImageUrl;

                 await _userManager.UpdateAsync(user);

                 return View(editVM);
             }

             user.City = editVM.City;
             user.State = editVM.State;
             user.Pace = editVM.Pace;
             user.Mileage = editVM.Mileage;

             await _userManager.UpdateAsync(user);

             return RedirectToAction("Detail", "User", new { user.Id });
         }*/
    }
}
