using Gymbuddy.Entities;

namespace Gymbuddy.ViewModels
{
    public class AdministrationViewModel
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
