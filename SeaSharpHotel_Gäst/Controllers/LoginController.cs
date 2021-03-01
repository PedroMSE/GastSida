using Microsoft.AspNetCore.Mvc;
using SeaSharpHotel_Gäst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace SeaSharpHotel_Gäst.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(GuestLogin guestLogin)
        {
            Guest validatedLogin = null;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(guestLogin), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:64133/api/Login/CheckLogin", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    validatedLogin = JsonConvert.DeserializeObject<Guest>(apiResponse);
                }
            }

            if (validatedLogin != null)
            {
                await SetGuestAuthenticated(validatedLogin);

                //Den ska inte vara med. Bara för att visa att det fungerar
                return Redirect("~/Profile/Index/" + validatedLogin.Id);
            }
            else
            {
                ModelState.AddModelError("", "Password or Email provided were wrong. Please try again.");
                return View();
            }
        }

        private async Task SetGuestAuthenticated(Guest validatedLogin)
        {
            // Allt stämmer, logga in användaren
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, validatedLogin.E_Mail));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Index(GuestLogin gastInfo, string returnUrl = null)
        //{

        //    bool gastGiltig = KontrolleraGast(gastInfo);
        //    if (gastGiltig==true)
        //    {
        //        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        //        identity.AddClaim(new Claim(ClaimTypes.Name, gastInfo.E_Mail));
        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        //        if (returnUrl != null)
        //        {
        //            return Redirect(returnUrl);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Gast");
        //        }
                    
        //    }
        //    ViewBag.FelMeddelande = "Login failed. Please try again";
        //    return View();
        //}
        //private bool KontrolleraGast(GuestLogin gastInfo)
        //{
        //    if (gastInfo.E_Mail == "Pedro" && gastInfo.Password=="Pass")
        //    {
        //        //gissar att vi behöver länk till inlogservice
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        
    }
}
