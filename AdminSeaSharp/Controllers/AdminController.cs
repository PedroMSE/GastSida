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

namespace AdminSeaSharp.Controllers
{
    //La till [Authorize] här så att inloggning för admin nu fungerar
    //[Authorize]
    public class AdminController : Controller
    {
        // GET: AdminController        
        public async Task<IActionResult> Index()
        {

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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Inlog");
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Guest guest)
        {
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
            //lägger in det sen
            /* try
             {

                 return RedirectToAction(nameof(Index));
             }
             catch
             {
                 return View();
             }*/
        }

        // GET: AdminController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
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
            Guest receivedGuest = new Guest();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync("http://193.10.202.78/GuestAPI/api/Guest/" + guest.Id, content))
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
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://193.10.202.78/GuestAPI/api/Guest/" + id))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                }
            }

            /*try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }*/
            return RedirectToAction("Index");
        }
    }
}
