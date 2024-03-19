using Microsoft.EntityFrameworkCore;
using Models.HazarNabeg;
using Models.Poke;
using System.Drawing;
namespace DataBaseContext
{
    public class ApplicationContext : DbContext
    {
        //Хазарский набег
        public DbSet<HazarNabeg> HazarNabegs { get; set; } = null!;
        public DbSet<Quastionnaire> Quastionnaires { get; set; } = null!;
        public DbSet<HazarUser> HazarUsers { get; set; } = null!;
        public DbSet<TestHazar> TestHazars { get; set; } = null!;

        //Тыкалка

        public DbSet<UserPoke> Users { get; set; } = null!;
        public DbSet<PointPoke> EmotionsPoke { get; set; } = null!;
        public DbSet<TestPoke> TestPokes{ get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
