using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AdminSeaSharp.Models;
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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminSeaSharp.Controllers
{
    //La till [Authorize] här så att inloggning för admin nu fungerar
    [Authorize]
    public class AdminController : Controller
    {

        
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        // GET: AdminController        
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Admin index");
            List<Guest> guests = new List<Guest>();
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://193.10.202.78/GuestAPI/api/Guest");
            string jsonresponse = await response.Content.ReadAsStringAsync();
            guests = JsonConvert.DeserializeObject<List<Guest>>(jsonresponse);

            return View(guests);
        }
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword

        public async Task<ActionResult> SignOut()
        #pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            _logger.LogInformation("Admin Signout");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Inlog");
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            _logger.LogInformation("Admin Details");
            return View();
        }

        // GET: AdminController/Create
        public IActionResult Create()
        {
            List<Categories> categorieses = new List<Categories>();
            categorieses.Add(new Categories { Category = "Standard" });
            categorieses.Add(new Categories { Category = "VIP" });
            ViewData["Status"] = new SelectList(categorieses, "Category", "Category");

            List<Categories> categories = new List<Categories>();
            categories.Add(new Categories { Category = "Standard" });
            categories.Add(new Categories { Category = "Organisation" });
            ViewData["Type"] = new SelectList(categories, "Category", "Category");
            _logger.LogInformation("Admin Create");
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Guest guest)
        {
            _logger.LogInformation("Admin Create Metod");
            
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
            return RedirectToAction("Index", "Admin");
        }

        // GET: AdminController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            _logger.LogInformation("Admin Edit Sida");
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

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guest guest)
        {
            _logger.LogInformation("Admin Edit Metod");
            Guest receivedGuest = new Guest();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://193.10.202.78/GuestAPI/api/Guest/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedGuest = JsonConvert.DeserializeObject<Guest>(apiResponse);
                }
            }
            return RedirectToAction("Index", "Admin");
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete()
        {
            _logger.LogInformation("Admin Delete Sida");
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Admin Delete Metod");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://193.10.202.78/GuestAPI/api/Guest/" + id))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
