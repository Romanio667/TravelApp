using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TravelApp.Data;
using TravelApp.Interfaces;
using TravelApp.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TravelApp.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IPlaceRepository _placeRepository;
        IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITipRepository _tipRepository;

        public PlaceController(IPlaceRepository placeRepository, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor, ITipRepository tipRepository)
        {
            _placeRepository = placeRepository;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _tipRepository = tipRepository;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<Place> places;

            if (!string.IsNullOrEmpty(searchString))
            {
                // Фильтрация по городу или стране (в данном случае по названию места)
                places = await _placeRepository.GetPlaceByCity(searchString);
                ViewData["SearchString"] = searchString;
            }
            else
            {
                places = await _placeRepository.GetAll();
                ViewData["SearchString"] = "";
            }

            return View(places);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Place place = await _placeRepository.GetByIdAsync(id);
            if (place == null)
            {
                return NotFound(); // Место не найдено
            }

            // Загрузка советов для этого места
            var tips = await _tipRepository.GetTipsForPlace(id);
            place.Tips = tips.ToList(); // Явное преобразование в List<Tip>

            return View(place);
        }


        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createPlaceViewModel = new PlaceImageConvert { AppUserId = curUserId };
            return View(createPlaceViewModel);
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
                AppUserId = place.AppUserId,
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddTip(int placeId, string tipContent)
        {
            var place = await _placeRepository.GetByIdAsync(placeId);
            if (place == null)
            {
                return NotFound(); // Место не найдено
            }

            // Получаем ID текущего аутентифицированного пользователя
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tip = new Tip
            {
                Content = tipContent,
                PlaceId = placeId,
                UserId = userId // Устанавливаем свойство UserId
            };

            await _tipRepository.Add(tip);

            // Возвращаем JSON-ответ с информацией о добавленном совете
            return RedirectToAction("Detail", new { id = placeId });
        }


        // Добавление метода удаления tip
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteTip(int tipId)
        {
            var tip = await _tipRepository.GetByIdAsync(tipId);
            if (tip == null)
            {
                // Возвращаем JSON с информацией о том, что Tip не был найден
                return Json(new { success = false, message = "Tip not found" });
            }

            int placeId = tip.PlaceId; // Сохраняем ID места, к которому относится tip
            await _tipRepository.Delete(tipId); // Передаем только ID вместо объекта tip

            // Возвращаем JSON с информацией об успешном удалении и ID места для перенаправления
            return Json(new { success = true, placeId = placeId });
        }
    }
}
