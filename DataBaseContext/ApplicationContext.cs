using Microsoft.EntityFrameworkCore;
using Models.HazarNabeg;
namespace DataBaseContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<HazarNabeg> HazarNabegs { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
