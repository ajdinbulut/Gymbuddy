using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Infrastructure
{
    partial class GymDBSeed
    {
        
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
                    Email = "ajdinbulut@gmail.com"


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
                    Id = 1,
                    UserId = 1,
                    RoleId = 2
                }
            });

        }

    }
}
