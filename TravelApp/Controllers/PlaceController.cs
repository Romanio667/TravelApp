using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TravelApp.Data;
using TravelApp.Interfaces;
using TravelApp.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TravelApp.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IPlaceRepository _placeRepository;
        IWebHostEnvironment _hostEnvironment;

        public PlaceController(IPlaceRepository placeRepository, IWebHostEnvironment hostEnvironment)
        {
            _placeRepository = placeRepository;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Place> places = await _placeRepository.GetAll();
            return View(places);
        }
        
        public async Task<IActionResult> Detail(int id) 
        {

            Place place = await _placeRepository.GetByIdAsync(id);

            return View(place);
        }

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlaceImageConvert place)
        {
            String filename = "";

            if (!ModelState.IsValid) 
            { 
                return View(place);
            }
            if (place.photo != null)
            {
                String uploadfolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + place.photo.FileName;
                String filepath = Path.Combine(uploadfolder, filename);
                place.photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }

            Place p = new Place
            {

                Title = place.Title,
                Description = place.Description,
                Image = filename,
                Address = place.Address,
                PlaceCategory = place.PlaceCategory,
            };
            _placeRepository.Add(p);


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var place = await _placeRepository.GetByIdAsync(id);
            if (place == null)
            {
                return View("Error");
            }

            var placeEditModel = new PlaceEditModel
            {
                Title = place.Title,
                Description = place.Description,
                AddressId = place.AddressId,
                Address = place.Address,
                PlaceCategory = place.PlaceCategory,
            };
            return View(placeEditModel);
        }

        // POST: /Place/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(PlaceEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var place = await _placeRepository.GetByIdAsync(model.Id);
            if (place == null)
            {
                return View("Error");
            }

            place.Title = model.Title;
            place.Description = model.Description;
            place.AddressId = model.AddressId;
            place.Address = model.Address;
            place.PlaceCategory = model.PlaceCategory;

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
                place.Image = filename;
            }

            _placeRepository.Update(place);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var placeDetails = await _placeRepository.GetByIdAsync(id);
            if(placeDetails == null) return View("Error");
            return View(placeDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var placeDetails = await _placeRepository.GetByIdAsync(id);
            if (placeDetails == null) return View("Error");

            _placeRepository.Delete(placeDetails);
            return RedirectToAction("Index");
        }

    }
}
