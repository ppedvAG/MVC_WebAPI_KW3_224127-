using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeoApp.Shared.Entities;

namespace GeoApp.Api.Data
{
    public class GeoAppContext : DbContext
    {
        public GeoAppContext (DbContextOptions<GeoAppContext> options)
            : base(options)
        {
        }

        public DbSet<GeoApp.Shared.Entities.Continent> Continent { get; set; } = default!;

        public DbSet<GeoApp.Shared.Entities.Country> Country { get; set; } = default!;
    }
}
