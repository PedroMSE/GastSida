using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SeaSharpHotel_Gäst.Models
{
    public class GuestLogin
    {
        [Required(ErrorMessage = "Enter your email")]
        [Display(Name ="Email")]

        public string E_Mail { get; set; }

        [Required(ErrorMessage = "Enter your Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}