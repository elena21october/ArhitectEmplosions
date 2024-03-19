using Microsoft.EntityFrameworkCore;
using DataBaseContext;

namespace ArhitectEmplosions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin",
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader();
                                  });
            });
			//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
			builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
            
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.UseCors();

            app.Run();
        }
    }
}
