using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorAdvanced.Models;

namespace RazorAdvanced.Data
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext (DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public DbSet<RazorAdvanced.Models.Movie> Movie { get; set; } = default!;
    }
}
