using System.Reflection.Emit;
using Buytopia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Buytopia.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mark CompanyRegistrationViewModel as keyless
            modelBuilder.Entity<CompanyRegistrationViewModel>().HasNoKey();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }


    }
}
