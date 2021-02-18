using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SeaSharpHotel_Gäst.Models
{
    public class InlogModel
    {
        [Required(ErrorMessage = "Enter your email")]
        [Display(Name ="Email")]

        public string GastMejl { get; set; }

        [Required(ErrorMessage = "Enter your Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]

        public string Losenord { get; set; }
    }
}