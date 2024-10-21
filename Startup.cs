using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // Add services for MVC
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Detailed error pages in development
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Custom error handling
                app.UseHsts(); // Enable HTTP Strict Transport Security
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting(); // Enable routing middleware

            app.UseAuthorization(); // Enable authorization middleware

            app.UseEndpoints(endpoints =>
            {
                // Default route for the HomeController
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); // Route for Home
            });
        }
    }
}

