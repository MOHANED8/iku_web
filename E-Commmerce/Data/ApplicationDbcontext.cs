<<<<<<< HEAD
﻿using E_Commmerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using E_Commmerce.ViewModels;
=======
﻿using E_Commmerce.Models; // Models for ApplicationUser, Product, and Category
using Microsoft.AspNetCore.Identity; // For Identity-related classes
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // For IdentityDbContext
using Microsoft.EntityFrameworkCore; // For DbContext functionalities
using E_Commmerce.ViewModels; // For RoleViewModel
>>>>>>> 69e884f (Initial project upload)

namespace E_Commmerce.Data
{
    // Custom DbContext inheriting from IdentityDbContext for application-specific database management
    public class ApplicationDbcontext : IdentityDbContext<ApplicationUser>
    {
        // DbSet for managing Product entities
        public DbSet<Product> Products { get; set; } = null!;

        // DbSet for managing Category entities
        public DbSet<Category> Categories { get; set; } = null!;

<<<<<<< HEAD
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>()
        .HasIndex(u => u.Email)
        .IsUnique();
        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
=======
        // Constructor to pass DbContext options to the base class
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options) : base(options)
        {
        }
>>>>>>> 69e884f (Initial project upload)

        // Configures the database schema and seed data
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Calls the base implementation to configure Identity-related tables
            base.OnModelCreating(builder);

            // Ensure the Email column in the ApplicationUser table is unique
            builder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Rename default Identity tables
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            // Seed initial categories into the database
            builder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Electronics" },
                    new Category { Id = 2, Name = "Clothes" },
                    new Category { Id = 3, Name = "Shoes" },
                    new Category { Id = 4, Name = "Books" },
                    new Category { Id = 5, Name = "Furniture" },
                    new Category { Id = 6, Name = "Accessories" },
                    new Category { Id = 7, Name = "Beauty" },
                    new Category { Id = 8, Name = "Sports" },
                    new Category { Id = 9, Name = "Health" },
                    new Category { Id = 10, Name = "Toys" },
                    new Category { Id = 11, Name = "Food" },
                    new Category { Id = 12, Name = "Beverages" },
                    new Category { Id = 13, Name = "Home" },
                    new Category { Id = 14, Name = "Garden" },
                    new Category { Id = 15, Name = "Tools" },
                    new Category { Id = 16, Name = "Automotive" }
<<<<<<< HEAD

                    );

        builder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Product>().HasData(
    
);
=======
                );

            // Configure the relationship between Category and Product
            builder.Entity<Category>()
                .HasMany(c => c.Products) // One Category has many Products
                .WithOne(p => p.Category) // One Product belongs to one Category
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete when a Category is deleted

            // Configure seed data for Product (placeholder for actual seed data)
            builder.Entity<Product>().HasData(
            // Add product seed data here if needed
            );
        }

        // DbSet for RoleViewModel to support role management
        public DbSet<RoleViewModel> RoleViewModel { get; set; } = default!;
>>>>>>> 69e884f (Initial project upload)
    }
}
