using Microsoft.EntityFrameworkCore;
using GarageManager.Models;

namespace GarageManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    }
}
