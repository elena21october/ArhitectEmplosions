using Microsoft.EntityFrameworkCore;
using Models.HazarNabeg;
using System.Drawing;
namespace DataBaseContext
{
    public class ApplicationContext : DbContext
    {
        //Хазарский набег
        public DbSet<HazarNabeg> HazarNabegs { get; set; } = null!;
        public DbSet<Quastionnaire> Quastionnaires { get; set; } = null!;
        public DbSet<Emotion> Emotions { get; set; } = null!;
        public DbSet<Differentiation> Differentiations { get; set; } = null!;
        public DbSet<PointHN> Points { get; set; } = null!;
        public DbSet<HazarUser> HazarUsers { get; set; } = null!;


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
