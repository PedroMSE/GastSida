using AdminSeaSharp.Models;
using Microsoft.AspNetCore.Mvc;
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

namespace AdminSeaSharp.Controllers
{
    public class InlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Inlog adminInfo, string returnUrl = null)
        {
            //vi tror att här ska länkas till inlogservicen..
            bool AdminGiltig = KontrolleraAdmin(adminInfo);
            if (AdminGiltig == true)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, adminInfo.UserName));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (returnUrl != null)

                {  
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }

            }
            ViewBag.FelMeddelande = "Login failed. Please try again";
            return View();
        }
        private bool KontrolleraAdmin(Inlog adminInfo)
        {    //anrop till webbservicen grupp3. skicka med adminInfo


            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(adminInfo), Encoding.UTF8, "application/json");
                LoginResponse recievedResponse; 
                using (var response = httpClient.PostAsync("https://informatik10.ei.hv.se/UserService/Login", content))
                {
                    string apiResponse = response.Result.Content.ReadAsStringAsync().ToString();

                    recievedResponse = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);


                    if (recievedResponse.Status == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
        }

    }
}

