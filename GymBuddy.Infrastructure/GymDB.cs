﻿
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
        }
        public DbSet<User> Users { get; set; }
        public DbSet<CompetingUser> CompetingUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserCountry> UserCountries { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostLikes> PostLikes { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Connection> Connection { get; set; }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Ajdin",
                    LastName = "Bulut",
                    Username = "admin",
                    PasswordHash = "WudiKaNkYoEQks/Svc3U21g6iQxpmyr3ndgx4Iauxpo=",
                    PasswordSalt = "W3/rItNCU3JGwjC/mAsgLg==",
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
