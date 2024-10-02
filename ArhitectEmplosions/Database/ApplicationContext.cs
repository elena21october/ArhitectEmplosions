using ArhitectEmplosions.Models;
using ArhitectEmplosions.Models.HazarNabeg;
using ArhitectEmplosions.Models.Poke;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ArhitectEmplosions.Database
{
    public class ApplicationContext : DbContext
    {
        //Хазарский набег
        public DbSet<HazarNabeg> HazarNabegs { get; set; } = null!;
        public DbSet<Quastionnaire> Quastionnaires { get; set; } = null!;
        public DbSet<EmotionPoke> EmotionPokes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Testing> Testing { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
