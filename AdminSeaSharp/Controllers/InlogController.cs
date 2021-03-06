﻿using AdminSeaSharp.Models;
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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace AdminSeaSharp.Controllers
{
    public class InlogController : Controller
    {
        private readonly ILogger<InlogController> _logger;

        public InlogController(ILogger<InlogController> logger)
        {
            _logger = logger;
        }
            //get: InlogController
            public IActionResult Index()
        {
            _logger.LogInformation("Admin Inlog Sida");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Inlog adminInfo)
        {
            _logger.LogInformation("Admin Inlog Metod");
            LoginResponse validatedInlog = null;//= new User();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(adminInfo), Encoding.UTF8, "application/json");
                 
                using (var response= await httpClient.PostAsync("https://informatik10.ei.hv.se/UserService/Login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    validatedInlog = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);
                }
            }
            if (validatedInlog !=null)
            {
                if (validatedInlog.Status == true)
                {
                    if (validatedInlog.Role.Contains("GuestAdmin"))
                    {
                        await SetUserAuthenticated(adminInfo.UserName);
                        return Redirect("~/Admin/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Inloggningen är inte godkänd");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Inloggningen är inte godkänd");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Inloggningen är inte godkänd");
                return View();

            }      
        }
        private async Task SetUserAuthenticated(string userName)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, userName));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }
    }
}

