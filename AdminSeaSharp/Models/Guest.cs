using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AdminSeaSharp.Models
{
    public class Guest
    {

        public int Id { get; set; }

        [DisplayName("Förnamn")]
        public string Firstname { get; set; }

        [DisplayName("Efternamn")]
        public string Lastname { get; set; }

        [DisplayName("Adress")]
        public string Street_Adress { get; set; }

        [DisplayName("Postnummer")]
        public int PostalCode { get; set; }

        [DisplayName("Stad")]
        public string City { get; set; }

        [DisplayName("Telefonnummer")]
        public string Phonenumber { get; set; }

        [DisplayName("Typ")]
        public string Type { get; set; }

        public string Status { get; set; }

        [DisplayName("Email")]
        public string E_Mail { get; set; }

        [DisplayName("Lösenord")]
        public string Password { get; set; }

    }
}
