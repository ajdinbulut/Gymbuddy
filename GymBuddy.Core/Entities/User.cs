﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gymbuddy.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public List<UserRole>? UserRoles { get; set; }

    }
}
