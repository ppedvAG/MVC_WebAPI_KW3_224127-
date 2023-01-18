using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoutingSamples.Models;

namespace RoutingSamples.Data
{
    public class MoveDbContext : DbContext
    {
        public MoveDbContext (DbContextOptions<MoveDbContext> options)
            : base(options)
        {
        }

        public DbSet<RoutingSamples.Models.Movie> Movie { get; set; } = default!;
    }
}
