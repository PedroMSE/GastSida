using Microsoft.AspNetCore.Mvc;
using SeaSharpHotel_Gäst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace SeaSharpHotel_Gäst.Controllers
{
    public class InlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(InlogModel gastInfo, string returnUrl = null)
        {

            bool gastGiltig = KontrolleraGast(gastInfo);
            if (gastGiltig==true)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, gastInfo.GastMejl));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                    
            }
            ViewBag.FelMeddelande = "Login failed. Please try again";
            return View();
        }
        private bool KontrolleraGast(InlogModel gastInfo)
        {
            if (gastInfo.GastMejl=="Pedro" && gastInfo.Losenord=="Pass")
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
