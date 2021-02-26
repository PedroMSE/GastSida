using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdminSeaSharp.Models;

namespace AdminSeaSharp.Data
{
    public class Context : DbContext
    {

        public DbSet<AdminSeaSharp.Models.Guest> Guest { get; set; }

        public Context (DbContextOptions<Context> options) : base (options)
        {

        }

    }
}
