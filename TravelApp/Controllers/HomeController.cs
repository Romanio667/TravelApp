using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using TravelApp.Helpers;
using TravelApp.Interfaces;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlaceRepository _placeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IPlaceRepository placeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _placeRepository = placeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeViewModel();
            try
            {
                string url = "https://ipinfo.io?token=07dd3dbd8e92c7";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.Country = ipInfo.Country;
                homeViewModel.City = ipInfo.City;    
                if(homeViewModel.City != null)
                {
                    homeViewModel.Places = await _placeRepository.GetPlaceByCity(homeViewModel.City);
                }
                else
                {
                    homeViewModel.Places = null;
                }
                return View(homeViewModel);
            }
            catch (Exception ex) 
            {
                homeViewModel.Places = null;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
