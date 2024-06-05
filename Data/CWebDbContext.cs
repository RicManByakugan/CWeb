using CWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CWeb.Data
{
    public class CWebDbContext : DbContext
    {
        public CWebDbContext(DbContextOptions<CWebDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patient { get; set; } = default!;
        public DbSet<Personnel> Personnel { get; set; } = default!;
        public DbSet<Activite> Acitivite { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;
	}
}
