using E_Commmerce.Data; // For ApplicationDbcontext
using E_Commmerce.IReposatory; // For IRepository interfaces
using E_Commmerce.IReposatory.RepositoryModels; // For repository implementations
using E_Commmerce.Models; // For ApplicationUser and Identity models
using Microsoft.AspNetCore.Identity; // For Identity services
using Microsoft.EntityFrameworkCore; // For EF Core functionalities

namespace E_Commmerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
            // Add services to the container
            builder.Services.AddControllersWithViews();

            // Add database context
            builder.Services.AddDbContext<ApplicationDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));

            // Add Identity services
=======
            // Add services to the container (Dependency Injection)
            builder.Services.AddControllersWithViews(); // Add MVC services

            // Add database context and configure SQL Server connection
            builder.Services.AddDbContext<ApplicationDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"))); // Connection string named "DB"

            // Configure Identity services for authentication and authorization
>>>>>>> 69e884f (Initial project upload)
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbcontext>() // Use ApplicationDbcontext for Identity storage
                .AddDefaultTokenProviders(); // Adds default token providers (e.g., for password reset)

<<<<<<< HEAD
            // Add repositories
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IPaymentHistoryRepository, PaymentHistoryRepository>();

            // Add session and distributed memory cache services
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddDistributedMemoryCache();

            var app = builder.Build();

            // Seed Admin User and Role
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedAdminAsync(services);
            }

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
=======
            // Register repository services for Dependency Injection
            builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Product repository
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); // Category repository
            builder.Services.AddScoped<IPaymentHistoryRepository, PaymentHistoryRepository>(); // Payment history repository

            // Configure session services
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout after 30 minutes of inactivity
                options.Cookie.HttpOnly = true; // Makes the cookie accessible only via HTTP
                options.Cookie.IsEssential = true; // Marks the cookie as essential
            });

            builder.Services.AddDistributedMemoryCache(); // Adds distributed memory cache for session storage

            var app = builder.Build(); // Build the application

            // Seed admin user and roles during application startup
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedAdminAsync(services); // Call method to seed admin role and user
>>>>>>> 69e884f (Initial project upload)
            }

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error"); // Redirects to error page in production
                app.UseHsts(); // Enforces HTTPS for security
            }

<<<<<<< HEAD
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
=======
            app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
            app.UseStaticFiles(); // Serve static files (e.g., CSS, JS, images)

            app.UseRouting(); // Enable routing
            app.UseSession(); // Enable session management
            app.UseAuthentication(); // Enable authentication middleware
            app.UseAuthorization(); // Enable authorization middleware
>>>>>>> 69e884f (Initial project upload)

            // Define the default route pattern
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Category}/{action=UserIndex}/{id?}");

            app.Run(); // Run the application
        }

        // Add additional services to IServiceCollection
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // Add MVC services

            // Configure session services
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout after 30 minutes
                options.Cookie.HttpOnly = true; // Makes the cookie HTTP only
                options.Cookie.IsEssential = true; // Mark the cookie as essential
            });

            services.AddDistributedMemoryCache(); // Add in-memory cache for session management
        }

        // Method to seed default admin role and user
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>(); // Role manager service
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>(); // User manager service

            // Define default roles
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                // Check if the role exists, create it if not
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed admin user
            const string adminEmail = "admin@example.com"; // Admin email
            const string adminPassword = "Admin@123"; // Admin password

            if (await userManager.FindByEmailAsync(adminEmail) == null) // Check if admin user exists
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true // Confirm email by default
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword); // Create admin user

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin"); // Assign admin role to user
                }
            }
        }

        // Add services to IServiceCollection
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddDistributedMemoryCache();
        }

        // Seed Admin User and Role
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            const string adminRole = "Admin";
            const string adminEmail = "admin@example.com";
            const string adminPassword = "Admin@123";

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }
        }
    }
}
