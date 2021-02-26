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

namespace AdminSeaSharp.Controllers
{
    //La till [Authorize] här så att inloggning för admin nu fungerar
    [Authorize]
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
        public ActionResult Create(Guest guest)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        { 
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
