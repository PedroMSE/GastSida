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
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SeaSharpHotel_Gäst.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
            public IActionResult Index()
        {
            _logger.LogInformation("Guest Login Sida");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(GuestLogin guestLogin)
        {
            _logger.LogInformation("Guest Login Metod");
            Guest validatedLogin = null;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(guestLogin), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://193.10.202.78/GuestAPI/api/Login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    validatedLogin = JsonConvert.DeserializeObject<Guest>(apiResponse);
                }
            }
            if (validatedLogin != null)

            { 
                await SetGuestAuthenticated(validatedLogin);
                //Den ska inte vara med. Bara för att visa att det fungerar
                return Redirect("~/Guest/Index/" + validatedLogin.Id);
            }
            else
            {
                ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
                return View();
            }
        }

        private async Task SetGuestAuthenticated(Guest validatedLogin)
        {
            _logger.LogInformation("Guest Validation");
            // Allt stämmer, logga in användaren
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, validatedLogin.E_Mail));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }
    }
}
