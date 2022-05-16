using Gymbuddy.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gymbuddy
{
    public class GymDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CompetingUser> CompetingUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=GymBuddy;Username=postgres;Password=postgres");

    }
}
