using AdminSeaSharp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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

            bool gastGiltig = KontrolleraAdmin(adminInfo);
            if (gastGiltig == true)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, adminInfo.UserName));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (returnUrl != null)

                {  //vi tror att här ska länkas till inlogservicen..
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
            if (adminInfo.UserName == "Alto" && adminInfo.Password == "Password")
            { //
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}

