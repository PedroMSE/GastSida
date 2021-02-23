using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeaSharpHotel_Gäst.Models;

namespace AdminSeaSharp.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<SeaSharpHotel_Gäst.Models.Guest> Guest { get; set; }
    }
}
