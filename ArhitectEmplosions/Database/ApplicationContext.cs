using ArhitectEmplosions.Models;
using ArhitectEmplosions.Models.HazarNabeg;
using ArhitectEmplosions.Models.Poke;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ArhitectEmplosions.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<HazarNabeg> HazarNabegs { get; set; } = null!;
        public DbSet<Quastionnaire> Quastionnaires { get; set; } = null!;
        public DbSet<EmotionPoke> EmotionPokes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Testing> Testing { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<EmotionForm> EmotionForms { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            const string adminLogin = "admin";
            string adminPassword = "1qaz!QAZ1qaz";
            string adminName = "Mukimovv D.V.";

            string mainLogin = "Rasuleva";
            string mainPassword = "q1w2e3";
            string mainName = "Rasuleva U.V.";

            string mainLogin2 = "Ovechkina";
            string mainPassword2 = "q1w2e3";
            string mainName2 = "Ovechkina E.K.";

            // добавляем роли
            Role adminRole = new Role
            {
                Id = 1, 
                Name = adminRoleName
            };
            Role userRole = new Role
            {
                Id = 2,
                Name = userRoleName
            };
            User adminUser = new User
            {
                Id = 1, 
                Name = adminName, 
                Login = adminLogin, 
                Password = adminPassword, 
                RoleId = adminRole.Id
            };
            User mainUser = new User
            {
                Id = 2, 
                Name = mainName, 
                Login = mainLogin, 
                Password = mainPassword, 
                RoleId = adminRole.Id
            };
            User mainUser2 = new User
            {
                Id = 3, 
                Name = mainName2, 
                Login = mainLogin2, 
                Password = mainPassword2, 
                RoleId = adminRole.Id
            };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser, mainUser, mainUser2 });
            base.OnModelCreating(modelBuilder);
        }

        public void DropDataBase()
        {
            Database.EnsureDeleted();
        }
    }
}
