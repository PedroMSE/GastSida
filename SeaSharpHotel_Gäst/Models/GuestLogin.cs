using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SeaSharpHotel_Gäst.Models
{
    public class GuestLogin
    { 
        public string E_Mail { get; set; }

        public string Password { get; set; }
    }
}