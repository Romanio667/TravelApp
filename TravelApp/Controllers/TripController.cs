using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Data;
using TravelApp.Interfaces;
using TravelApp.Models;
using TravelApp.Repository;

namespace TravelApp.Controllers
{
    public class TripController : Controller
    {
        private readonly ITripRepository _tripRepository;
        IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TripController(ITripRepository tripRepository, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _tripRepository = tripRepository;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<Trip> trips;

            if (!string.IsNullOrEmpty(searchString))
            {
                // Фильтрация по стране или городу отправления
                trips = await _tripRepository.GetAllTripsByCity(searchString);
                ViewData["SearchString"] = searchString; 
            }
            else
            {
                trips = await _tripRepository.GetAll();
                ViewData["SearchString"] = "";
            }

            return View(trips);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Trip trip = await _tripRepository.GetByIdAsync(id);
            return View(trip);
        }

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createTripViewModel = new TripImageConvert { AppUserId = curUserId };
            return View(createTripViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripImageConvert trip)
        {
            String filename = "";

            if (!ModelState.IsValid)
            {
                return View(trip);
            }
            if (trip.photo != null)
            {
                String uploadfolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + trip.photo.FileName;
                String filepath = Path.Combine(uploadfolder, filename);
                trip.photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            Trip t = new Trip
            {
                Title = trip.Title,
                Description = trip.Description,
                Image = filename,
                TripFee = trip.TripFee,
                Currency = trip.Currency,
                TripAddress = trip.TripAddress,
                TripCategory = trip.TripCategory,
                AppUserId = trip.AppUserId,
            };
            _tripRepository.Add(t);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var trip = await _tripRepository.GetByIdAsync(id);
            if (trip == null)
            {
                return View("Error");
            }

            var tripEditModel = new TripEditModel
            {
                Title = trip.Title,
                Description = trip.Description,
                TripFee = trip.TripFee,
                Currency = trip.Currency,
                TripAddressId = trip.TripAddressId,
                TripAddress = trip.TripAddress,
                TripCategory = trip.TripCategory,
            };
            return View(tripEditModel);
        }

        // POST: /Trip/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(TripEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var trip = await _tripRepository.GetByIdAsync(model.Id);
            if (trip == null)
            {
                return View("Error");
            }

            trip.Title = model.Title;
            trip.Description = model.Description;
            trip.TripFee = model.TripFee;
            trip.Currency = model.Currency;
            trip.TripAddressId = model.TripAddressId;
            trip.TripAddress = model.TripAddress;
            trip.TripCategory = model.TripCategory;

            // Update the image only if a new file is uploaded
            if (model.ImageFile != null)
            {
                string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                string filename = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filepath = Path.Combine(uploadFolder, filename);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                trip.Image = filename;
            }

            _tripRepository.Update(trip);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tripDetails = await _tripRepository.GetByIdAsync(id);
            if (tripDetails == null) return View("Error");
            return View(tripDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var tripDetails = await _tripRepository.GetByIdAsync(id);
            if (tripDetails == null) return View("Error");

            _tripRepository.Delete(tripDetails);
            return RedirectToAction("Index");
        }
    }
}

