using AuthenticationOne.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationOne.DBContext
{
    public class AppDBContext:IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { 
        
        }

        public DbSet<AppUser> AppUsers { get; set; } = null;
        public DbSet<Company> companies { get; set; } = null;

        //add seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data for the Company table
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    ID = 1,
                    Name = "Company 1",
                    type = "Type A",
                    Description = "Description for Company 1",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Company
                {
                    ID = 2,
                    Name = "Company 2",
                    type = "Type B",
                    Description = "Description for Company 2",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            // Add more seed data as needed
            );
        }
    }
}
