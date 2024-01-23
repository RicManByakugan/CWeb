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

        public DbSet<Accueil> Accueil { get; set; } = default!;
        public DbSet<Patient> Patient { get; set; } = default!;
        public DbSet<Personnel> Personnel { get; set; } = default!;
        public DbSet<Service> Service { get; set; } = default!;
    }
}
