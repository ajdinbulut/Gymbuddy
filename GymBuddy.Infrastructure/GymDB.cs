
using Gymbuddy.Core.Entities;
using GymBuddy.Core.Entities;
using GymBuddy.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gymbuddy.Infrastructure
{
    public class GymDB : DbContext
    {

        public GymDB(DbContextOptions<GymDB> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            SeedData(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<CompetingUser> CompetingUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserCountry> UserCountries { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "admin",
                    Username = "admin",
                    Password = "admin123",
                    Age = 21,
                    Email = "ajdinbulut@gmail.com",


                }
                );
            modelBuilder.Entity<Role>().HasData(new List<Role>{
                new Role
                {
                    Id = 1,
                    Name = "User"
                },
                new Role
                {
                    Id = 2,
                    Name = "Admin"
                }
            });
            modelBuilder.Entity<UserRole>().HasData(new List<UserRole>
            {
                new UserRole
                {
                    Id = 1,
                    UserId = 1,
                    RoleId = 1
                },
                new UserRole
                {
                    Id = 2,
                    UserId = 1,
                    RoleId = 2
                }
            });

        }
    }
}
