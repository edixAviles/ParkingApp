using Microsoft.EntityFrameworkCore;

namespace KrugerApp.Models
{
    public partial class KrugerAppContext : DbContext
    {
        public KrugerAppContext(DbContextOptions<KrugerAppContext> options) : base(options)
        {
        }

        public DbSet<VehicleType> MVehicleType { get; set; }
        public DbSet<Vehicle> MVehicle { get; set; }
        public DbSet<Customer> MCustomer { get; set; }
        public DbSet<Parking> MParking { get; set; }
    }
}
