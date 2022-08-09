
using Gymbuddy.Core.Entities;
using GymBuddy.Core.Entities;
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
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<CompetingUser> CompetingUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserCountry> UserCountries { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

       
    }
}
