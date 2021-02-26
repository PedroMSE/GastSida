using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdminSeaSharp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace AdminSeaSharp.Models
{
    public class Inlog
    {
        [Required(ErrorMessage = "Enter your email")]
        [Display(Name = "Email")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}
