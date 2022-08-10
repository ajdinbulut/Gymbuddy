using Gymbuddy.Core.Entities;

namespace Gymbuddy.ViewModels
{
    public class RegisterViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string name { get; set; }     
        public int age { get; set; }
        public string ProfileURL { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
