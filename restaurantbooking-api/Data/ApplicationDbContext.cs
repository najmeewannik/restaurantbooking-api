using Microsoft.EntityFrameworkCore;
using restaurantbooking_api.Models;

namespace restaurantbooking_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Table> Tables => Set<Table>();
        public DbSet<Booking> Bookings => Set<Booking>();

    }
}
