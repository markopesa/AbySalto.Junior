using AbySalto.Junior.Infrastructure.Database;
using AbySalto.Junior.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Junior
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapGet("/", () => Results.Redirect("/Restaurant"));

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Restaurant}/{action=Index}/{id?}");

            app.Run();
        }
    }
}