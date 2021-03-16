using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SeaSharpHotel_Gäst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Newtonsoft.Json.Converters;
using System.Web;
using Microsoft.Extensions.Logging;

namespace SeaSharpHotel_Gäst.Controllers
{
    [Authorize]
    public class GuestController : Controller
    {
        private readonly ILogger<GuestController> _logger;

        public GuestController(ILogger<GuestController> logger)
        {
            _logger = logger;
        }

        // GET: ProfileController        
        public async Task<IActionResult> Index(int id)
        {
            _logger.LogInformation("Guest Index Sida");
            Guest currentGuest = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.78/GuestAPI/api/Guest/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    currentGuest = JsonConvert.DeserializeObject<Guest>(apiResponse);
                }
            }
            return View(currentGuest);
        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword

        public async Task<ActionResult> SignOut()
        #pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            _logger.LogInformation("Guest Signout");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Guest Edit Sida");
            Guest guest = new Guest();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://193.10.202.78/GuestAPI/api/Guest/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    guest = JsonConvert.DeserializeObject<Guest>(apiResponse);
                }
            }
            return View(guest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guest guest)
        {
            _logger.LogInformation("Guest Edit Metod");
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://193.10.202.78/GuestAPI/api/Guest/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index", "Guest", new { id = guest.Id });
        }
        
            
            
               

               
            

        
        
    }
}
