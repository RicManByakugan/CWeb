using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CWeb.Models;

namespace CWeb.Data
{
    public class CWebContext : DbContext
    {
        public CWebContext (DbContextOptions<CWebContext> options)
            : base(options)
        {
        }

        public DbSet<CWeb.Models.Personnel> Personnel { get; set; } = default!;
    }
}
