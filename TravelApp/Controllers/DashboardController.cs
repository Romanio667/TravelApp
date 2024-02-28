using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using TravelApp.Data;
using TravelApp.Interfaces;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var userTrips = await _dashboardRepository.GetAllUserTrips();
            var userClubs = await _dashboardRepository.GetAllUserPlaces();
            var dashboardViewModel = new DashboardViewModel()
            {
                Trips = userTrips,
                Places = userClubs
            };
            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = curUserId,
                Age = user.Age,
                ProfileImageURL = user.ProfileImageURL,
                Country = user.Country,
                City = user.City,
            };
            return View(editUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            AppUser user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                user.Id = editVM.Id;
                user.Age = editVM.Age;
                user.Country = editVM.Country;
                user.City = editVM.City;

                // If a new image is uploaded, update the ProfileImageURL
                if (editVM.Image != null)
                {
                    // Handle image upload logic (similar to what you did in PlaceController)
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    string filename = Guid.NewGuid().ToString() + "_" + editVM.Image.FileName;
                    string filepath = Path.Combine(uploadFolder, filename);
                    using (var stream = new FileStream(filepath, FileMode.Create))
                    {
                        await editVM.Image.CopyToAsync(stream);
                    }
                    user.ProfileImageURL = filename;
                }

                _dashboardRepository.Update(user);

                return RedirectToAction("Index"); // Redirect to the dashboard or another appropriate action
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                ModelState.AddModelError("", "An error occurred while editing the profile.");
                return View("EditUserProfile", editVM);
            }
        }
             
    }
}
