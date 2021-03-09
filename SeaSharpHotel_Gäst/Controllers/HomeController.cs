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

namespace SeaSharpHotel_Gäst.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BliMedlem()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BliMedlem(Guest guest)
        {
            Guest receivedGuest = new Guest();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(guest), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://193.10.202.78/GuestAPI/api/Guest", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    receivedGuest = JsonConvert.DeserializeObject<Guest>(apiResponse);
                    ViewData["Succes"] = "Your";
                } 
            }
            return RedirectToAction("Index", "Home");
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
