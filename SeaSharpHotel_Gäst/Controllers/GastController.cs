using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeaSharpHotel_Gäst.Controllers
{
    [Authorize]
    public class GastController : Controller
    {        
        public IActionResult Index()
        {
            return View();
        }


#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public async Task<ActionResult> SignOut()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");

        }
    }
}
