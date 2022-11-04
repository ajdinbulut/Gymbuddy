using Gymbuddy.Core.Entities;

namespace Gymbuddy.ViewModels
{
    public class EditAccViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }
        public int RoleId { get; set; }
        public bool Selected { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<SelectedRoleViewModel> selectedRole { get; set; }
    }
}
