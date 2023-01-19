using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI_ControllerSamples.Models;

namespace WebAPI_ControllerSamples.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext (DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPI_ControllerSamples.Models.Movie> Movie { get; set; } = default!;
    }
}
