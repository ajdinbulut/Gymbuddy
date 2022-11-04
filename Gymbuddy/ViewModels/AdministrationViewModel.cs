using Gymbuddy.Core.Entities;

namespace Gymbuddy.ViewModels
{
    public class AdministrationViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public List<Role> Roles { get; set; }
    }
}
