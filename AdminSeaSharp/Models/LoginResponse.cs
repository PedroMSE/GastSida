using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminSeaSharp.Models
{
    public class LoginResponse
    {
        public bool Status { get; set; }

        public List<String> Role { get; set; }
    }
}
