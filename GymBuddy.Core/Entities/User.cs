using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gymbuddy.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string? ProfilePhoto { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public List<UserRole>? UserRoles { get; set; }

    }
}
