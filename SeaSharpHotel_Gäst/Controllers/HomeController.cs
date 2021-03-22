using Microsoft.AspNetCore.Mvc;
using SeaSharpHotel_Gäst.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SeaSharpHotel_Gäst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Guest Home Index");
            return View();
        }
        public IActionResult BliMedlem()
        {
            _logger.LogInformation("Guest Signup Sida");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BliMedlem(Guest guest)
        {
            _logger.LogInformation("Guest Signup Metod");
            guest.Status = "Standard";
            guest.Type = "Standard";
            Guest receivedGuest = new Guest();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://193.10.202.78/GuestAPI/api/Guest", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedGuest = JsonConvert.DeserializeObject<Guest>(apiResponse);
                } 
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Restaurants()
        {
            return View();
        }
        public IActionResult Activities()
        {
            return View();
        }
        public IActionResult Rooms()
        {
            return View();
        }
        /*
        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */
    }
}
