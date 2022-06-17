using Gymbuddy.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gymbuddy
{
    public class GymDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CompetingUser> CompetingUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=GymBuddy;Username=postgres;Password=postgres");

    }
}
